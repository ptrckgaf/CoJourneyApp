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
    public class JourneyListViewModel : ViewModelBase, IJourneyListViewModel
    {
        public ObservableCollection<JourneyListModel> Journeys { get; set; } = new();
        private readonly IMediator _mediator;
        private readonly JourneyFacade _journeyFacade;

        public JourneyListViewModel(JourneyFacade journeyFacade, IMediator mediator)
        {
            _journeyFacade = journeyFacade;
            _mediator = mediator;

            _mediator.Register<UpdateMessage<JourneyListModel>>(JourneyUpdated);
            _mediator.Register<DeleteMessage<JourneyListModel>>(JourneyDeleted);
        }

        private async void JourneyUpdated(UpdateMessage<JourneyListModel> _) => await LoadAsync();
        private async void JourneyDeleted(DeleteMessage<JourneyListModel> _) => await LoadAsync();

        public async Task LoadAsync()
        {
            Journeys.Clear();
            var journeys = await _journeyFacade.GetAsync();
            Journeys.AddRange(journeys);
        }
    }
}
