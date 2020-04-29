using System;
using System.Windows.Input;


namespace ProjectB.ViewModel.Commands
{
    class CommandHandlerExecParameter : ICommand
    {
        private readonly Action<bool> _action;
        private readonly Func<bool, bool> _canExecute;

        public CommandHandlerExecParameter(Action<bool> action, Func<bool, bool> canExecute)
        {
            _action = action;
            _canExecute = canExecute;
        }


        public event EventHandler CanExecuteChanged
        {
            add
            {
                CommandManager.RequerySuggested += value;
            }
            remove
            {
                CommandManager.RequerySuggested -= value;
            }
        }

        public bool CanExecute(object parameter)
        {
            return _canExecute.Invoke((bool)parameter);
        }

        public void Execute(object parameter)
        {
            _action((bool)parameter);
        }
    }
}
