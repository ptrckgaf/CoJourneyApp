using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Controls;
using System.Windows.Input;
using CoJourney.App.Factories;
using CoJourney.App.Messages;
using CoJourney.App.Services;
using CoJourney.App.Views;
using CoJourney.App.Wrappers;
using CoJourney.App.Commands;
using CoJourney.BL.Models;
using CoJourney.DAL.Seeds;

namespace CoJourney.App.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private readonly IMediator _mediator;
        public MainViewModel(IUserListViewModel userListViewModel, ICarListViewModel carListViewModel, 
            IJourneyListViewModel journeyListViewModel, ICarEventListViewModel carEventListViewModel,
            IInvitationListViewModel invitationEventListViewModel, IFactory<IUserDetailViewModel> userDetailViewModelFactory,
            IFactory<ICarDetailViewModel> carDetailViewModelFactory,
            IMediator mediator)
        {
            UserListViewModel = userListViewModel;
            CarListViewModel = carListViewModel;
            JourneyListViewModel = journeyListViewModel;
            CarEventListViewModel = carEventListViewModel;
            InvitationEventListViewModel = invitationEventListViewModel;

            JourneyListViewControl.DataContext = JourneyListViewModel;
            UserListViewControl.DataContext = UserListViewModel;
            CarListViewControl.DataContext = CarListViewModel;
            CarEventListViewControl.DataContext = CarEventListViewModel;
            InvitationListViewModelControl.DataContext = InvitationEventListViewModel;

            SetListToUser = new RelayCommand(SetUserListView);
            SetListToCar = new RelayCommand(SetCarListView);
            SetListToJourney = new RelayCommand(SetJourneyListView);
            SetListToCarEvent = new RelayCommand(SetCarEventListView);
            SetListToInvitation = new RelayCommand(SetInvitationListView);

            mediator.Register<SelectedMessage<UserWrapper>>(OnUserSelected);
            mediator.Register<NewMessage<UserWrapper>>(OnUserAdd);

            mediator.Register<SelectedMessage<CarWrapper>>(OnCarSelected);
            mediator.Register<NewMessage<CarWrapper>>(OnCarAdd);
            
            _mediator = mediator;

            _userDetailViewModelFactory = userDetailViewModelFactory;
            _carDetailViewModelFactory = carDetailViewModelFactory;
            loggedUser = UserSeeds.User1.Id;
            carListViewModel.LoggedUser = loggedUser;
        }

        public readonly Guid loggedUser;

        public ObservableCollection<IUserDetailViewModel> UserDetailViewModels { get; } =
            new ObservableCollection<IUserDetailViewModel>();
        //public ObservableCollection<ICarDetailViewModel> CarDetailViewModels { get; } =
        //    new ObservableCollection<ICarDetailViewModel>();


        private readonly IFactory<IUserDetailViewModel> _userDetailViewModelFactory;
        private readonly IFactory<ICarDetailViewModel> _carDetailViewModelFactory;
        private IUserDetailViewModel? SelectedUserViewModel { get; set; }
        private ICarDetailViewModel? SelectedCarViewModel { get; set; }
        public UserListView UserListViewControl { get; } = new ();
        public CarListView CarListViewControl { get; } = new ();
        public JourneyListView JourneyListViewControl { get; } = new();
        public CarEventListView CarEventListViewControl { get; } = new();
        public InvitationListView InvitationListViewModelControl { get; } = new();
        public IUserListViewModel UserListViewModel { get; }
        public ICarListViewModel CarListViewModel { get; }
        public IJourneyListViewModel JourneyListViewModel { get; }
        public ICarEventListViewModel CarEventListViewModel { get; }
        public IInvitationListViewModel InvitationEventListViewModel { get; }
        public ICommand SetListToUser { get; }
        public ICommand SetListToCar { get; }
        public ICommand SetListToJourney { get; }
        public ICommand SetListToCarEvent { get; }
        public ICommand SetListToInvitation { get; }

        private void OnUserSelected(SelectedMessage<UserWrapper> message)
        {
            SetUserDetailModelView(message.Id);
        }
        private void OnCarSelected(SelectedMessage<CarWrapper> message)
        {
            SetCarDetailModelView(message.Id);
        }
        public UserDetailView UserDetailViewControl { get; } = new ();
        public CarDetailView CarDetailViewControl { get; } = new();

        private UserControl? _listControl;
        private UserControl? _modelControl;
        public UserControl? ListControl
        {
            get => _listControl;
            set
            {
                _listControl = value;
                OnPropertyChanged();
            }
        }
        public UserControl? ModelControl
        {
            get => _modelControl;
            set
            {
                _modelControl = value;
                OnPropertyChanged();
            }
        }
        private void SetUserListView()
        {
            ListControl = UserListViewControl;
            ModelControl = null;
        }
        private void SetCarListView()
        {
            ListControl = CarListViewControl;
            ModelControl = null;
        }
        private void SetJourneyListView()
        {
            ListControl = JourneyListViewControl;
            ModelControl = null;
        }
        private void SetCarEventListView()
        {
            ListControl = CarEventListViewControl;
            ModelControl = null;
        }
        private void SetInvitationListView()
        {
            ListControl = InvitationListViewModelControl;
            ModelControl = null;
        }
        
        private void OnUserAdd(NewMessage<UserWrapper> _) => SetUserDetailModelView(Guid.Empty);
        private void SetUserDetailModelView(Guid? id)
        {
            if (id == null)
            {
                SelectedUserViewModel = null;
            }
            else
            {
                var userDetail = UserDetailViewModels.SingleOrDefault(viewModel => viewModel.Model?.Id == id);
                if (userDetail == null)
                {
                    userDetail = _userDetailViewModelFactory.Create();
                    UserDetailViewModels.Add(userDetail);
                    userDetail.LoadAsync(id.Value);
                }

                SelectedUserViewModel = userDetail;
                UserDetailViewControl.DataContext = SelectedUserViewModel;
                ModelControl = UserDetailViewControl;
            }
        }
        private void OnCarAdd(NewMessage<CarWrapper> _) => SetCarDetailModelView(Guid.Empty);
        private void SetCarDetailModelView(Guid? id)
        {
            var carDetail = _carDetailViewModelFactory.Create();
            
            SelectedCarViewModel = carDetail;
            CarDetailViewControl.DataContext = SelectedCarViewModel;
            ModelControl = CarDetailViewControl;
            _mediator.Send(new LoadMessage<CarWrapper> {Id=id, TargetId = loggedUser});
        }
    }
}
