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
        private readonly UsersFacade _userFacade;
        public JourneyDetailViewModel(
            JourneyFacade journeyFacade, CarFacade carFacade,
            IMediator mediator, UsersFacade userFacade,
            IFactory<IJourneyDetailViewModel> journeyDetailViewModelFactory)
        {
            _journeyFacade = journeyFacade;
            _carFacade = carFacade;
            _userFacade = userFacade;
            _mediator = mediator;

            SaveCommand = new AsyncRelayCommand(SaveAsync, CanSave);
            DeleteCommand = new AsyncRelayCommand(DeleteAsync);
            SelectedCarChangedCommand = new RelayCommand(SelectedCarChanged);

            _journeyDetailViewModelFactory = journeyDetailViewModelFactory;

            _mediator.Register<LoadMessage<JourneyWrapper>>(async message => await JourneyLoad(message));
            LeaveCommand = new AsyncRelayCommand(LeaveJourney);
            JoinCommand = new AsyncRelayCommand(JoinToJoureny);
        }
        public Guid LoggedUser { get; 
            set; } = Guid.Empty;
        public JourneyWrapper? Model { get; set; } = null;
        public ICommand SaveCommand { get; }
        public ICommand DeleteCommand { get; }
        public ICommand SelectedCarChangedCommand { get; }
        public ICommand JoinCommand { get; }
        public ICommand LeaveCommand { get; }
        public CarListModel selectedCar { get; set; }
        public bool IsMyJourney => ((((Model == null)? Guid.Empty:Model.DriverId) == LoggedUser) || Model == null || Model.Id == Guid.Empty);
        public bool IsNotMyJourney => !IsMyJourney;
        public bool? CanJoin
        {
            get => (Model == null? -1:Model.CarCapacity) > 0 && IsNotMyJourney;
        }

        private async Task LoadCars(Guid ownerId)
        {
            var cars = await _carFacade.GetMyCarsAsync(ownerId);
            DriverCars.AddRange(cars);
        }

        private async Task JourneyLoad(LoadMessage<JourneyWrapper> message)
        {
            if (message.TargetId != null)
                LoggedUser = message.TargetId.Value;
            if (message.Id == Guid.Empty || message.Id is null)
            {
                Model = new JourneyWrapper(JourneyDetailModel.Empty);
                await LoadCars(LoggedUser);
                if (DriverCars.Count != 0)
                    selectedCar = DriverCars[0];
                
                Model.DriverId = LoggedUser;
                UsersDetailModel? user = await _userFacade.GetAsync(LoggedUser) ?? UsersDetailModel.Empty;
                Model.DriverName = user.Name;
                Model.DriverSurname = user.Surname;
            }
            else
            {
                var journeyDetail = await _journeyFacade.GetAsync(message.Id.Value);
                Model = journeyDetail;
                if (Model.DriverId != null)
                {
                    DriverCars.Clear();
                    await LoadCars(Model.DriverId.Value);
                    foreach (CarListModel car in DriverCars)
                    {
                        if (car.Id == Model.CarId)
                        {
                            selectedCar = car;
                            break;
                        }
                    }
                }
            }

        }

        private async Task LeaveJourney()
        {
            UsersDetailModel? usersDetail = await _userFacade.GetAsync(LoggedUser);
            if (usersDetail == null)
            {
                MessageBox.Show("Nebylo možné, se odhlásit!",
                    "Error!", MessageBoxButton.YesNo, MessageBoxImage.Error);
                return;
            }

            foreach (var journey in usersDetail.CoRidingJourneys)
            {
                if (journey.Id == Model.Id)
                {
                    usersDetail.CoRidingJourneys.Remove(journey);
                    break;
                }
            }
            usersDetail.CoRidingJourneys.Remove(Model);
            try
            {
                await _userFacade.SaveAsync(usersDetail);
            }
            catch 
            {
                MessageBox.Show("Nebylo možné se Odhlásit.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            
            foreach (var coRider in Model.CoRiders)
            {
                if (coRider.Id == LoggedUser)
                {
                    Model.CoRiders.Remove(coRider);
                    break;
                }
            }
            await SaveAsync();
            MessageBox.Show("Odhlášení proběhlo úspěšně.", "Podařilo se", MessageBoxButton.OK, MessageBoxImage.Exclamation);
        }

        private async Task JoinToJoureny()
        {
            UsersDetailModel? usersDetail = await _userFacade.GetAsync(LoggedUser);
            if (usersDetail == null)
            {
                MessageBox.Show("Nebylo možné, se do jízdy přidat!",
                    "Error!", MessageBoxButton.YesNo, MessageBoxImage.Error);
                return;
            }
            usersDetail.CoRidingJourneys.Add(Model);
            try
            {
                await _userFacade.SaveAsync(usersDetail);
            }
            catch
            {
                MessageBox.Show("Nebylo možné se registrovat.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            
            await SaveAsync();
            MessageBox.Show("Registrace proběhla úspěšně.", "Podařilo se", MessageBoxButton.OK, MessageBoxImage.Exclamation);
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
                if (MessageBoxResult.Yes != MessageBox.Show("Opravdu chcete danou jízdu smazat?",
                        "Pozor!", MessageBoxButton.YesNo, MessageBoxImage.Asterisk))
                    return;

                try
                {
                    await _journeyFacade.DeleteAsync(Model!.Id);
                }
                catch
                {
                    MessageBox.Show("Jízda nemohla být smazána.", "Chyba!", MessageBoxButton.OK,
                        MessageBoxImage.Error);
                }

                _mediator.Send(new DeleteMessage<JourneyWrapper>
                {
                    Model = Model
                });
            }

        }

        private void SelectedCarChanged()
        {
            if(Model != null)
                Model.CarId = selectedCar.Id;
        }
    }
}