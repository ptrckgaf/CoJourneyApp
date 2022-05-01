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
using CoJourney.App.Commands;
using System.Windows.Input;
using System.Windows;

namespace CoJourney.App.ViewModels
{
    public class LoginWindowViewModel : ViewModelBase
    {
        public ObservableCollection<UsersListModel> Users { get; set; } = new();
        private readonly IMediator _mediator;
        private readonly UsersFacade _userFacade;

        public LoginWindowViewModel(UsersFacade userFacade, IMediator mediator)
        {
            _userFacade = userFacade;
            _mediator = mediator;
            ChooseClickCommand = new RelayCommand<Window>(ChooseClick);
        }
        public ICommand ChooseClickCommand { get;}
        //private async void InvitationUpdated(UpdateMessage<InvitationListModel> _) => await LoadAsync();
        //private async void InvitationDeleted(DeleteMessage<InvitationListModel> _) => await LoadAsync();

        public async void LoadAsync()
        {
            Users.Clear();
            
            var users = await _userFacade.GetAsync();

            Users.AddRange(users);
            OnPropertyChanged();
        }

        public UsersListModel? SelectedUser { get; set; }
        public bool AllowStart { get; private set; } = false;
        public bool IsUserSelecetd { get => SelectedUser != null; }
        public void ChooseClick(Window loginWindow)
        {
            AllowStart = true;
            loginWindow.Close();
        }
    }
}
