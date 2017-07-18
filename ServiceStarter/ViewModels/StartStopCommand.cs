using System;
using System.Windows.Input;

namespace ServiceStarter.ViewModels
{
    public sealed class StartStopCommand<T> : ICommand
    {
        private readonly Action<T> _action;
        private readonly Predicate<T> _canExecute;

        public StartStopCommand(Action<T> methodToExecute) : this(methodToExecute, null) { }

        public StartStopCommand(Action<T> methodToExecute, Predicate<T> canExecuteEvaluator)
        {
            _action = methodToExecute ?? throw new ArgumentNullException("methodToExecute", "Delegate commands can not be null");
            _canExecute = canExecuteEvaluator;
        }

#pragma warning disable 0067
        public event EventHandler CanExecuteChanged = delegate { };
#pragma warning restore 0067

        public bool CanExecute(object parameter)
        {
            if (_canExecute == null)
            {
                return true;

            }
            else
            {
                return _canExecute((T)parameter);
            }
        }

        public void Execute(object parameter)
        {
            _action((T)parameter);
        }
    }
}
