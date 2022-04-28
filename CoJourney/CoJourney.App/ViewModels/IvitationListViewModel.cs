using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CoJourney.App.Extensions;
using CoJourney.App.Messages;
using CoJourney.App.Services;
using CoJourney.BL.Facades;
using CoJourney.BL.Models;

namespace CoJourney.App.ViewModels
{
    public class InvitationListViewModel : ViewModelBase, IInvitationListViewModel
    {
        public ObservableCollection<InvitationListModel> Invitations { get; set; } = new();
        private readonly IMediator _mediator;
        private readonly InvitationFacade _invitationFacade;

        public InvitationListViewModel(InvitationFacade invitationFacade, IMediator mediator)
        {
            _invitationFacade = invitationFacade;
            _mediator = mediator;

            _mediator.Register<UpdateMessage<InvitationListModel>>(InvitationUpdated);
        }

        private async void InvitationUpdated(UpdateMessage<InvitationListModel> _) => await LoadAsync();

        public async Task LoadAsync()
        {
            Invitations.Clear();
            var invitations = await _invitationFacade.GetAsync();
            Invitations.AddRange(invitations);
        }
    }
}
