using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using CoJourney.App.Messages;
using CoJourney.App.Services;
using CoJourney.App.Wrappers;
using CoJourney.BL.Facades;
using CoJourney.BL.Models;
using CoJourney.App.Commands;
using CoJourney.App.Factories;
using CoJourney.DAL.Seeds;

namespace CoJourney.App.ViewModels
{
    public class CarDetailViewModel : ViewModelBase, ICarDetailViewModel
    {
        private readonly IMediator _mediator;
        private readonly CarFacade _carFacade;
        private readonly UsersFacade _userFacade;
        private IFactory<ICarDetailViewModel> _carDetailViewModelFactory;

        public CarDetailViewModel(
            CarFacade carFacade,
            IMediator mediator, UsersFacade userFacade,
            IFactory<ICarDetailViewModel> carDetailViewModelFactory)
        {
            _carFacade = carFacade;
            _mediator = mediator;
            _userFacade = userFacade;

            SaveCommand = new AsyncRelayCommand(SaveAsync, CanSave);
            DeleteCommand = new AsyncRelayCommand(DeleteAsync);
            _carDetailViewModelFactory = carDetailViewModelFactory;
            _mediator.Register<LoadMessage<CarWrapper>>(async message => await CarLoad(message));
        }

        public Guid? OwnerId { get; set; }
        public CarWrapper? Model { get; set; }
        public ICommand SaveCommand { get; }
        public ICommand DeleteCommand { get; }


        private async Task CarLoad(LoadMessage<CarWrapper> message)
        {
            if (message.Id == Guid.Empty || message.Id is null)
            {
                Model = new CarWrapper(CarDetailModel.Empty);
            }
            else
            {
                var carDetail = await _carFacade.GetAsync(message.Id.Value);
                Model = carDetail;
            }

            OwnerId = message.TargetId;
        }

        public async Task SaveAsync()
        {
            if (Model == null)
                throw new NoNullAllowedException("Null model nemůže být uložen ani upraven.");

            Model.OwnerId = OwnerId;
            Model = await _carFacade.SaveAsync(Model.Model);
            _mediator.Send(new UpdateMessage<CarWrapper> {Model = Model});
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
                    await _carFacade.DeleteAsync(Model!.Id);
                }
                catch
                {
                    MessageBox.Show("Auto nemohlo být smazáno.", "Chyba!", MessageBoxButton.OK,
                        MessageBoxImage.Error);
                }

                _mediator.Send(new DeleteMessage<CarWrapper>
                {
                    Model = Model
                });
            }

        }

        public void IsAllowedInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }
    }
}