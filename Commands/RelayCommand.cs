using System;
using System.Windows.Input;

namespace HandbrakeAutomatorMVVM.Commands
{
  internal class RelayCommand : ICommand
  {
    readonly Func<bool> _canExecuteFunc;
    readonly Action _executeAction;

    public RelayCommand(Action executeAction, Func<bool> canExecute)
    {
      _executeAction = executeAction;
      _canExecuteFunc = canExecute;
    }

    public bool CanExecute(object? parameter) => _canExecuteFunc.Invoke();

    public void Execute(object? parameter) => _executeAction.Invoke();

    public event EventHandler? CanExecuteChanged
    {
      add { CommandManager.RequerySuggested += value; }
      remove { CommandManager.RequerySuggested -= value; }
    }
  }
}
