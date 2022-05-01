using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using CoJourney.App.Commands;
using CoJourney.App.Extensions;
using CoJourney.App.Messages;
using CoJourney.App.Services;
using CoJourney.App.Wrappers;
using CoJourney.BL.Facades;
using CoJourney.BL.Models;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace CoJourney.App.ViewModels
{
    public class CarEventListViewModel : ViewModelBase, ICarEventListViewModel
    {
        public ObservableCollection<CarEventListModel> CarEvents { get; set; } = new();
        private readonly IMediator _mediator;
        private readonly CarEventFacade _carEventFacade;

        public CarEventListViewModel(CarEventFacade carEventFacade, IMediator mediator)
        {
            _carEventFacade = carEventFacade;
            _mediator = mediator;

            _mediator.Register<UpdateMessage<CarEventWrapper>>(CarEventUpdated);
            _mediator.Register<DeleteMessage<CarEventWrapper>>(CarEventDeleted);
            _mediator.Register<SelectedMessage<CarEventListModel>>(CarEventListSelected);

            SelectedCarEventCommand = new RelayCommand<CarEventListModel>(SelectedCarEvent);
            NewCarEventCommand = new RelayCommand(NewCarEvent);
        }

        public int? SelectedIndex { get; set; }
        public Guid LoggedUserId { get; set; }
        private async void CarEventUpdated(UpdateMessage<CarEventWrapper> _) => await LoadAsync();
        private async void CarEventDeleted(DeleteMessage<CarEventWrapper> _) => await LoadAsync();

        public ICommand SelectedCarEventCommand { get; }
        public ICommand NewCarEventCommand { get; }

        public void SelectedCarEvent(CarEventListModel? carEventListModel) =>
            _mediator.Send(new SelectedMessage<CarEventWrapper> {Id = carEventListModel?.Id});

        public async Task LoadAsync()
        {
            int? tempSelectedIndex = SelectedIndex;
            CarEvents.Clear();
            var carEvents = await _carEventFacade.GetMyCarEventsAsync(LoggedUserId);
            CarEvents.AddRange(carEvents);
            SelectedIndex = tempSelectedIndex;
        }

        private void CarEventListSelected(SelectedMessage<CarEventListModel> message)
        {
            if (message.TargetId != null)
                LoggedUserId = message.TargetId.Value;
        }
        
        public void NewCarEvent() => _mediator.Send(new NewMessage<CarEventWrapper> {TargetId = LoggedUserId});
    }
}
