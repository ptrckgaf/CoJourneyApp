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
            IJourneyListViewModel journeyListViewModel, ICarEventListViewModel carEventListViewModel)
        {
            UserListViewModel = userListViewModel;
            CarListViewModel = carListViewModel;
            JourneyListViewModel = journeyListViewModel;
            CarEventListViewModel = carEventListViewModel; 

            JourneyListViewControl.DataContext = JourneyListViewModel;
            UserListViewControl.DataContext = UserListViewModel;
            CarListViewControl.DataContext = CarListViewModel;
            CarEventListViewControl.DataContext = CarEventListViewModel;

            SetListToUser = new RelayCommand(SetUserListView);
            SetListToCar = new RelayCommand(SetCarListView);
            SetListToJoureny = new RelayCommand(SetJourneyListView);
            SetListToCarEvent = new RelayCommand(SetCarEventListView);

            
        }

        public UserListView UserListViewControl = new ();
        public CarListView CarListViewControl = new ();
        public JourneyListView JourneyListViewControl = new();
        public CarEventListView CarEventListViewControl = new();
        public IUserListViewModel UserListViewModel { get; }
        public ICarListViewModel CarListViewModel { get; }
        public IJourneyListViewModel JourneyListViewModel { get; }
        public ICarEventListViewModel CarEventListViewModel { get; }
        public ICommand SetListToUser { get; }
        public ICommand SetListToCar { get; }
        public ICommand SetListToJoureny { get; }
        public ICommand SetListToCarEvent { get; }
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
    }
}
