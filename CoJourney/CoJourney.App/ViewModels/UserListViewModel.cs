using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using CoJourney.App.Extensions;
using CoJourney.App.Messages;
using CoJourney.App.Services;
using CoJourney.App.Wrappers;
using CoJourney.BL.Facades;
using CoJourney.BL.Models;
using CoJourney.DAL.Entities;
using Microsoft.Toolkit.Mvvm.Input;

namespace CoJourney.App.ViewModels
{
    public class UserListViewModel : ViewModelBase, IUserListViewModel
    {
        public ObservableCollection<UsersListModel> Users { get; set; } = new();
        private readonly IMediator _mediator;
        private readonly UsersFacade _usersFacade;

        public UserListViewModel(UsersFacade userFacade, IMediator mediator)
        {
            _usersFacade = userFacade;
            _mediator = mediator;

            SelectedUserCommand = new RelayCommand<UsersListModel>(SelectedUser);

            _mediator.Register<UpdateMessage<UserWrapper>>(UserUpdated);
            _mediator.Register<DeleteMessage<UserWrapper>>(UserDeleted);
        }

        private async void UserUpdated(UpdateMessage<UserWrapper> _) => await LoadAsync();
        private async void UserDeleted(DeleteMessage<UserWrapper> _) => await LoadAsync();
        public ICommand SelectedUserCommand { get; }

        public async Task LoadAsync()
        {
            Users.Clear();
            var users = await _usersFacade.GetAsync();
            Users.AddRange(users);
        }

        public void SelectedUser(UsersListModel? usersListModel) => 
            _mediator.Send(new SelectedMessage<UserWrapper> { Id = usersListModel?.Id });
    }
}
