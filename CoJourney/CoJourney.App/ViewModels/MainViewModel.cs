using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Input;
using CoJourney.App.Factories;
using CoJourney.App.Views;
using Microsoft.Toolkit.Mvvm.Input;

namespace CoJourney.App.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        public MainViewModel(IUserListViewModel userListViewModel, ICarListViewModel carListViewModel, 
            IJourneyListViewModel journeyListViewModel, ICarEventListViewModel carEventListViewModel,
            IInvitationListViewModel invitationEventListViewModel, IFactory<IUserDetailViewModel> userDetailViewModelFactory)
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

            _userDetailViewModelFactory = userDetailViewModelFactory;
        }
        public ObservableCollection<IUserDetailViewModel> UserDetailViewModels { get; } =
            new ObservableCollection<IUserDetailViewModel>();

        private readonly IFactory<IUserDetailViewModel> _userDetailViewModelFactory;
        private IUserDetailViewModel? SelectedViewModel { get; set; }
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

        //public ICommand Set
        public UserDetailView UserDetailViewControl { get; } = new ();

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

        private void SetUserDetailModelView(Guid? id)
        {
            if (id == null)
            {
                ModelControl = null;
                return;
            }
            else
            {
                var userDetail = UserDetailViewModels.SingleOrDefault(viewModel => viewModel.Model?.Id == id);
                if (userDetail == null)
                {
                    userDetail = _userDetailViewModelFactory.Create();
                    userDetail.LoadAsync(id.Value);
                    UserDetailViewModels.Add(userDetail);
                }

                SelectedViewModel = userDetail;
                UserDetailViewControl.DataContext = SelectedViewModel;
                ModelControl = UserDetailViewControl;
            }
        }
        
    }
}
