using System;
using System.Windows.Input;

namespace ShowMeLove.ViewModels
{
    public class RelayCommand : ICommand
    {
        private Action _action;
        private Func<bool> _canExecuteFunction;

        public event EventHandler CanExecuteChanged;

        public RelayCommand(Action action, Func<bool> canExecuteFunction = null)
        {
            _action = action;
            _canExecuteFunction = canExecuteFunction;
        }

        public bool CanExecute(object parameter)
        {
            if (_canExecuteFunction != null)
                return _canExecuteFunction.Invoke();

            return true;
        }

        public void Execute(object parameter)
        {
            _action.Invoke();
        }
    }
}
