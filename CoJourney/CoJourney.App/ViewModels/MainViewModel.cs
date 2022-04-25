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
        public MainViewModel()
        {
            SetListToUser = new RelayCommand(SetUserListView);
            SetListToCar = new RelayCommand(SetCarListView);
            SetListToJoureny = new RelayCommand(SetJourneyListView);
            SetListToCarEvent = new RelayCommand(SetCarEventListView);
        }

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
        private void SetUserListView()
        {
            ListControl = new UserListView();
            ListControl.DataContext = new UserListViewModel();
            //UserListViewModelControl.Users.Add(new UsersListModel("albrech", ""));
        }

        private void SetCarListView()
        {
            ListControl = new CarListView();
            ListControl.DataContext = new CarListViewModel();
        }
        private void SetJourneyListView()
        {
            ListControl = new JourneyListView();
            ListControl.DataContext = new JourneyListViewModel();
        }
        private void SetCarEventListView()
        {
            ListControl = new CarEvenetListView();
            ListControl.DataContext = new CarEventListViewModel();
        }
    }
}
