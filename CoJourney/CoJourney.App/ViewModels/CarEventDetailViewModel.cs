using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using CoJourney.App.Messages;
using CoJourney.App.Services;
using CoJourney.App.Wrappers;
using CoJourney.BL.Facades;
using CoJourney.BL.Models;
using CoJourney.App.Commands;

namespace CoJourney.App.ViewModels
{
    public class CarEventDetailViewModel : ViewModelBase, ICarEventDetailViewModel
    {
        private readonly IMediator _mediator;
        private readonly CarEventFacade _carEventFacade;

        public CarEventDetailViewModel(
            CarEventFacade CarEventFacade,
            IMediator mediator)
        {
            _carEventFacade = CarEventFacade;
            _mediator = mediator;

            SaveCommand = new AsyncRelayCommand(SaveAsync, CanSave);
            DeleteCommand = new AsyncRelayCommand(DeleteAsync);

            mediator.Register<LoadMessage<CarEventWrapper>>(async message => await CarEventLoad(message));
        }
        public Guid? LoggedUserId { get; private set; }
        public CarEventWrapper? Model { get; private set; }
        public ICommand SaveCommand { get; }
        public ICommand DeleteCommand { get; }


        public async Task LoadAsync(Guid id)
        {
            Model = await _carEventFacade.GetAsync(id) ?? CarEventDetailModel.Empty;
            OnPropertyChanged();
        }

        public async Task SaveAsync()
        {
            if (Model == null)
                throw new NoNullAllowedException("Null model nemůže být uložen ani upraven.");

            if (Model.Id == Guid.Empty)
            {
                Model.InstitutorId = LoggedUserId;
            }
            Model = await _carEventFacade.SaveAsync(Model.Model);
            _mediator.Send(new UpdateMessage<CarEventWrapper> {Model = Model});
        }

        private bool CanSave() => Model?.IsValid ?? false;

        public async Task DeleteAsync()
        {
            if (Model is null || Model.Id == Guid.Empty)
                throw new NoNullAllowedException("Null model nemůže být odstreněn.");

            if (Model.Id != Guid.Empty)
            {
                if (MessageBoxResult.Yes != MessageBox.Show("Opravdu chcete danou udalost smazat?",
                        "Pozor!",MessageBoxButton.YesNo, MessageBoxImage.Asterisk)) 
                    return;

                try
                {
                    await _carEventFacade.DeleteAsync(Model!.Id);
                }
                catch
                {
                    MessageBox.Show("Udalost nemohla být smazána.", "Chyba!", MessageBoxButton.OK,
                        MessageBoxImage.Error);
                }

                _mediator.Send(new DeleteMessage<CarEventWrapper>
                {
                    Model = Model
                });
            }
        }

        private async Task CarEventLoad(LoadMessage<CarEventWrapper> message)
        {
            if (message.TargetId != null)
                LoggedUserId = message.TargetId.Value;
            if (message.Id == Guid.Empty || message.Id is null)
            {
                Model = new CarEventWrapper(CarEventDetailModel.Empty);
            }
            else
            {
                var carDetail = await _carEventFacade.GetAsync(message.Id.Value);
                Model = carDetail;
            }
        }
    }
}