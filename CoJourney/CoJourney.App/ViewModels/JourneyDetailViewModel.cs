using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Markup.Localizer;
using CoJourney.App.Messages;
using CoJourney.App.Services;
using CoJourney.App.Wrappers;
using CoJourney.BL.Facades;
using CoJourney.BL.Models;
using CoJourney.App.Commands;
using CoJourney.App.Extensions;
using CoJourney.App.Factories;
using CoJourney.DAL.Seeds;
using Microsoft.Extensions.Logging.Abstractions;

namespace CoJourney.App.ViewModels
{
    public class JourneyDetailViewModel : ViewModelBase, IJourneyDetailViewModel
    {
        private readonly IMediator _mediator;
        private readonly JourneyFacade _journeyFacade;
        private IFactory<IJourneyDetailViewModel> _journeyDetailViewModelFactory;
        public ObservableCollection<CarListModel> DriverCars { get; set; } = new();
        private readonly CarFacade _carFacade;
        public JourneyDetailViewModel(
            JourneyFacade journeyFacade, CarFacade carFacade,
            IMediator mediator, UsersFacade userFacade,
            IFactory<IJourneyDetailViewModel> journeyDetailViewModelFactory)
        {
            _journeyFacade = journeyFacade;
            _carFacade = carFacade;
            _mediator = mediator;

            SaveCommand = new AsyncRelayCommand(SaveAsync, CanSave);
            DeleteCommand = new AsyncRelayCommand(DeleteAsync);
            _journeyDetailViewModelFactory = journeyDetailViewModelFactory;
            _mediator.Register<LoadMessage<JourneyWrapper>>(async message => await JourneyLoad(message));
        }
        public Guid LoggedUser { get; set; } = Guid.Empty;
        public JourneyWrapper? Model { get; set; } = null;
        public ICommand SaveCommand { get; }
        public ICommand DeleteCommand { get; }
        public CarListModel selectedCar { get; set; }
        public bool IsMyJourney => (((Model == null)? Guid.Empty:Model.DriverId) == LoggedUser);
        public bool IsNotMyJourney => !IsMyJourney;
        public bool? CanJoin
        {
            get => (Model == null? -1:Model.CarCapacity) > 0 && IsNotMyJourney;
        }

        private async Task JourneyLoad(LoadMessage<JourneyWrapper> message)
        {
            if (message.Id == Guid.Empty || message.Id is null)
            {
                Model = new JourneyWrapper(JourneyDetailModel.Empty);
            }
            else
            {
                var journeyDetail = await _journeyFacade.GetAsync(message.Id.Value);
                Model = journeyDetail;
                if (Model.DriverId != null)
                {
                    DriverCars.Clear();
                    var cars = await _carFacade.GetMyCarsAsync(Model.DriverId.Value);
                    DriverCars.AddRange(cars);
                    foreach (CarListModel car in cars)
                    {
                        if (car.Id == Model.CarId)
                        {
                            selectedCar = car;
                            break;
                        }
                    }
                    if (message.TargetId != null)
                        LoggedUser = message.TargetId.Value;
                }
            }

        }

        public async Task SaveAsync()
        {
            if (Model == null)
                throw new NoNullAllowedException("Null model nemůže být uložen ani upraven.");

            Model = await _journeyFacade.SaveAsync(Model.Model);
            _mediator.Send(new UpdateMessage<JourneyWrapper> {Model = Model});
        }

        private bool CanSave() => Model?.IsValid ?? false;

        public async Task DeleteAsync()
        {
            if (Model is null)
                throw new NoNullAllowedException("Null model nemůže být odstreněn.");

            if (Model.Id != Guid.Empty)
            {
                if (MessageBoxResult.Yes != MessageBox.Show("Opravdu chcete dané auto smazat?",
                        "Pozor!", MessageBoxButton.YesNo, MessageBoxImage.Asterisk))
                    return;

                try
                {
                    await _journeyFacade.DeleteAsync(Model!.Id);
                }
                catch
                {
                    MessageBox.Show("Auto nemohlo být smazáno.", "Chyba!", MessageBoxButton.OK,
                        MessageBoxImage.Error);
                }

                _mediator.Send(new DeleteMessage<JourneyWrapper>
                {
                    Model = Model
                });
            }

        }
    }
}