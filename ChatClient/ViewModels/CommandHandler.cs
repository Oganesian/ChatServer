namespace ChatClient.ViewModels
{
    using System;
    using System.Windows.Input;

    public class CommandHandler : ICommand
    {
        private Action _action;
        private Action<object> _actionWithParameter;
        private Func<bool> _canExecute;

        public CommandHandler(Action action, Func<bool> canExecute)
        {
            _action = action;
            _canExecute = canExecute;
        }

        public CommandHandler(Action<object> action, Func<bool> canExecute)
        {
            _actionWithParameter = action;
            _canExecute = canExecute;
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public bool CanExecute(object parameter)
        {
            if (_canExecute == null)
            {
                return true;
            }
            else
            {
                bool result = _canExecute.Invoke();
                return result;
            }
        }

        public void Execute(object parameter)
        {
            if (parameter != null)
            {
                _actionWithParameter(parameter);
            }
            else
            {
                _action();
            }
        }
    }
}
