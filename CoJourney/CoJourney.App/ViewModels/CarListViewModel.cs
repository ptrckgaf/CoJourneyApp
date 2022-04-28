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
    public class CarListViewModel : ViewModelBase, ICarListViewModel
    {
        public ObservableCollection<CarListModel> Cars { get; set; } = new();
        private readonly IMediator _mediator;
        private readonly CarFacade _carFacade;

        public CarListViewModel(CarFacade carFacade, IMediator mediator)
        {
            _carFacade = carFacade;
            _mediator = mediator;

            _mediator.Register<UpdateMessage<CarListModel>>(CarUpdated);
            _mediator.Register<DeleteMessage<CarListModel>>(CarDeleted);
        }

        private async void CarUpdated(UpdateMessage<CarListModel> _) => await LoadAsync();
        private async void CarDeleted(DeleteMessage<CarListModel> _) => await LoadAsync();

        public async Task LoadAsync()
        {
            Cars.Clear();
            var cars = await _carFacade.GetAsync();
            Cars.AddRange(cars);
        }
    }
}
