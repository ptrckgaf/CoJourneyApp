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

            _mediator.Register<UpdateMessage<CarEventListModel>>(CarEventUpdated);
            _mediator.Register<DeleteMessage<CarEventListModel>>(CarEventDeleted);
        }

        private async void CarEventUpdated(UpdateMessage<CarEventListModel> _) => await LoadAsync();
        private async void CarEventDeleted(DeleteMessage<CarEventListModel> _) => await LoadAsync();

        public async Task LoadAsync()
        {
            CarEvents.Clear();
            var carEvents = await _carEventFacade.GetAsync();
            CarEvents.AddRange(carEvents);
        }
    }
}
