using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using CoJourney.App.Commands;
using CoJourney.App.Views;
using CoJourney.BL.Models;
using Microsoft.EntityFrameworkCore;

namespace CoJourney.App.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        public MainViewModel(IUserListViewModel userListViewModel, ICarListViewModel carListViewModel, 
            IJourneyListViewModel journeyListViewModel, ICarEventListViewModel carEventListViewModel,
            IInvitationListViewModel invitationEventListViewModel)
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


        }

        public UserListView UserListViewControl = new ();
        public CarListView CarListViewControl = new ();
        public JourneyListView JourneyListViewControl = new();
        public CarEventListView CarEventListViewControl = new();
        public InvitationListView InvitationListViewModelControl = new();
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
        private UserControl? _listControl;
        public UserControl? ListControl
        {
            get => _listControl;
            set
            {
                _listControl = value;
                OnPropertyChanged();
            }
        }
        private void SetUserListView() => ListControl = UserListViewControl;
        

        private void SetCarListView()
        {
            ListControl = CarListViewControl;
        }
        private void SetJourneyListView()
        {
            ListControl = JourneyListViewControl;
        }
        private void SetCarEventListView()
        {
            ListControl = CarEventListViewControl;
        }
        private void SetInvitationListView()
        {
            ListControl = InvitationListViewModelControl;
        }
    }
}
