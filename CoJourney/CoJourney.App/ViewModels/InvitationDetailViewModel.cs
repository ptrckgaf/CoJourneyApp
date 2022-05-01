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
    public class InvitationDetailViewModel : ViewModelBase, IInvitationDetailViewModel
    {
        private readonly IMediator _mediator;
        private readonly InvitationFacade _invitationFacade;

        public InvitationDetailViewModel(
            InvitationFacade InvitationFacade,
            IMediator mediator)
        {
            _invitationFacade = InvitationFacade;
            _mediator = mediator;

            SaveCommand = new AsyncRelayCommand(SaveAsync, CanSave);
            DeleteCommand = new AsyncRelayCommand(DeleteAsync);
        }

        public InvitationWrapper? Model { get; private set; }
        public ICommand SaveCommand { get; }
        public ICommand DeleteCommand { get; }


        public async Task LoadAsync(Guid id)
        {
            Model = await _invitationFacade.GetAsync(id) ?? InvitationDetailModel.Empty;
            OnPropertyChanged();
        }

        public async Task SaveAsync()
        {
            if (Model == null)
                throw new NoNullAllowedException("Null model nemůže být uložen ani upraven.");
            

            Model = await _invitationFacade.SaveAsync(Model.Model);
            _mediator.Send(new UpdateMessage<InvitationWrapper> {Model = Model});
        }

        private bool CanSave() => Model?.IsValid ?? false;

        public async Task DeleteAsync()
        {
            if (Model is null || Model.Id == Guid.Empty)
                throw new NoNullAllowedException("Null model nemůže být odstreněn.");

            if (Model.Id != Guid.Empty)
            {
                if (MessageBoxResult.Yes != MessageBox.Show("Opravdu chcete danou pozvanku smazat?",
                        "Pozor!",MessageBoxButton.YesNo, MessageBoxImage.Asterisk)) 
                    return;

                try
                {
                    await _invitationFacade.DeleteAsync(Model!.Id);
                }
                catch
                {
                    MessageBox.Show("Pozvanka nemohla být smazána.", "Chyba!", MessageBoxButton.OK,
                        MessageBoxImage.Error);
                }

                _mediator.Send(new DeleteMessage<InvitationWrapper>
                {
                    Model = Model
                });
            }
        }
    }
}