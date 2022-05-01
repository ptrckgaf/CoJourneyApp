using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using CoJourney.App.Extensions;
using CoJourney.App.Messages;
using CoJourney.App.Services;
using CoJourney.App.Wrappers;
using CoJourney.BL.Facades;
using CoJourney.BL.Models;
using CoJourney.App.Commands;
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

            SelectedCarCommand = new RelayCommand<CarListModel>(SelectedCar);
            NewCarCommand = new RelayCommand(NewCar);

            _mediator.Register<UpdateMessage<CarWrapper>>(CarUpdated);
            _mediator.Register<DeleteMessage<CarWrapper>>(CarDeleted);
            _mediator.Register<SelectedMessage<CarListModel>>(CarListModeSelected);
        }
        public Guid LoggedUser { get; set; }
        private async void CarUpdated(UpdateMessage<CarWrapper> _) => await LoadAsync();
        private async void CarDeleted(DeleteMessage<CarWrapper> _) => await LoadAsync();
        private void CarListModeSelected(SelectedMessage<CarListModel> model) => LoggedUser = model.Id.Value;
        private void NewCar() => _mediator.Send(new NewMessage<CarWrapper>(){Id = Guid.Empty});
        public ICommand SelectedCarCommand { get; }
        public ICommand NewCarCommand { get; }
        public int? SelectedCarIndex { get; set; }

        public async Task LoadAsync()
        {
            int? selectedIndexBuffer = SelectedCarIndex;
            Cars.Clear();
            var cars = await _carFacade.GetMyCarsAsync(LoggedUser);
            Cars.AddRange(cars);
            SelectedCarIndex = selectedIndexBuffer;
        }
        public void SelectedCar(CarListModel? carListModel) =>
            _mediator.Send(new SelectedMessage<CarWrapper> { Id = carListModel?.Id });
    }
}
