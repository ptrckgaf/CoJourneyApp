using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Navigation;
using CoJourney.App.Commands;
using CoJourney.App.Extensions;
using CoJourney.App.Messages;
using CoJourney.App.Services;
using CoJourney.App.Wrappers;
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

            _mediator.Register<UpdateMessage<JourneyWrapper>>(JourneyUpdated);
            _mediator.Register<DeleteMessage<JourneyWrapper>>(JourneyDeleted);

            SelectedJourneyCommand = new RelayCommand<JourneyListModel>(SelectedJourney);
            NewJourneyCommand = new RelayCommand(NewJourney);
            ApplyFilterCommand = new AsyncRelayCommand(LoadAsync);
        }

        public void NewJourney() => _mediator.Send(new NewMessage<JourneyWrapper>(){Id = Guid.Empty});
        public DateTime BeginTimeFilter { get; set; } = DateTime.Now.AddYears(-50);
        public DateTime EndTimeFilter { get; set; } = DateTime.Now.AddYears(50);
        public string TargetLocation { get; set; } = "";
        public string StartLocation { get; set; } = "";
        private async void JourneyUpdated(UpdateMessage<JourneyWrapper> _) => await LoadAsync();
        private async void JourneyDeleted(DeleteMessage<JourneyWrapper> _) => await LoadAsync();
        public ICommand SelectedJourneyCommand { get; }
        public int? SelectedJourneyIndex { get; set; }
        public ICommand NewJourneyCommand { get; set; }
        public ICommand ApplyFilterCommand { get; set; }
        public async Task LoadAsync()
        {
            int ?tempSelectedJourneyIndex = SelectedJourneyIndex;
            Journeys.Clear();
            var journeys = await _journeyFacade.GetFilterdJoureneysAsync(BeginTimeFilter, EndTimeFilter,TargetLocation,StartLocation);
            Journeys.AddRange(journeys);
            SelectedJourneyIndex = tempSelectedJourneyIndex;
        }

        public void SelectedJourney(JourneyListModel? journeyListModel)
        {
            _mediator.Send(new SelectedMessage<JourneyWrapper> {Id = journeyListModel?.Id});
        }
    }
}
