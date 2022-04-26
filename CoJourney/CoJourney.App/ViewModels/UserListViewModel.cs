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
using CoJourney.DAL.Entities;

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

            _mediator.Register<UpdateMessage<UsersListModel>>(UserUpdated);
        }

        private async void UserUpdated(UpdateMessage<UsersListModel> _) => await LoadAsync();

        public async Task LoadAsync()
        {
            Users.Clear();
            var cars = await _usersFacade.GetAsync();
            Users.AddRange(cars);
        }

    }
}
