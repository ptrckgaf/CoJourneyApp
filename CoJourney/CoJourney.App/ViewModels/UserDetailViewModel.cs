using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using CoJourney.App.Messages;
using CoJourney.App.Services;
using CoJourney.App.Wrappers;
using CoJourney.BL.Facades;
using CoJourney.BL.Models;
using Microsoft.Toolkit.Mvvm.Input;

namespace CoJourney.App.ViewModels
{
    public class UserDetailViewModel : ViewModelBase, IUserDetailViewModel
    {
        private readonly IMediator _mediator;
        private readonly UsersFacade _userFacade;

        public UserDetailViewModel(
            UsersFacade userFacade,
            IMediator mediator)
        {
            _userFacade = userFacade;
            _mediator = mediator;

            SaveCommand = new AsyncRelayCommand(SaveAsync, CanSave);
            DeleteCommand = new AsyncRelayCommand(DeleteAsync);
        }

        public UserWrapper? Model { get; private set; }
        public ICommand SaveCommand { get; }
        public ICommand DeleteCommand { get; }


        public async Task LoadAsync(Guid id)
        {
            Model = await _userFacade.GetAsync(id) ?? UsersDetailModel.Empty;
        }

        public async Task SaveAsync()
        {
            if (Model == null)
                throw new NoNullAllowedException("Null model nemůže být uložen ani upraven.");
            

            Model = await _userFacade.SaveAsync(Model.Model);
            _mediator.Send(new UpdateMessage<UserWrapper> {Model = Model});
        }

        private bool CanSave() => Model?.IsValid ?? false;

        public async Task DeleteAsync()
        {
            if (Model is null)
                throw new NoNullAllowedException("Null model nemůže být uložen ani upraven.");
            

            if (Model.Id != Guid.Empty)
            {
                if (MessageBoxResult.Yes != MessageBox.Show("Opravdu chcete daného uživatele smazat?",
                        "Pozor!",MessageBoxButton.YesNo, MessageBoxImage.Asterisk)) 
                    return;

                try
                {
                    await _userFacade.DeleteAsync(Model!.Id);
                }
                catch
                {
                    MessageBox.Show("Uživatel nemohl být smazán.", "Chyba!", MessageBoxButton.OK,
                        MessageBoxImage.Error);
                    return;
                }

                _mediator.Send(new DeleteMessage<UserWrapper>
                {
                    Model = Model
                });
            }
        }
    }
}