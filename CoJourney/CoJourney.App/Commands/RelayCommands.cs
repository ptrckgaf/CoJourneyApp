using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CoJourney.App.Commands
{
    public class RelayCommand : ICommand
    {
        private readonly Microsoft.Toolkit.Mvvm.Input.RelayCommand _relayCommand;

        public RelayCommand(Action execute, Func<bool>? canExecute = null)
        {
            _relayCommand = canExecute is null ? new Microsoft.Toolkit.Mvvm.Input.RelayCommand(execute) : new Microsoft.Toolkit.Mvvm.Input.RelayCommand(execute, canExecute);
        }

        public bool CanExecute(object? parameter) => _relayCommand.CanExecute(parameter);

        public void Execute(object? parameter) => _relayCommand.Execute(parameter);

        public event EventHandler? CanExecuteChanged
        {
            add => CommandManager.RequerySuggested += value;
            remove => CommandManager.RequerySuggested -= value;
        }
    }
}
