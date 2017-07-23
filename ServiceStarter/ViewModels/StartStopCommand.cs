using System;
using System.Windows.Input;

namespace ServiceStarter.ViewModels {
    public sealed class StartStopCommand<T> : ICommand {
        private readonly Action<T> _action;
        private readonly Predicate<T> _canExecute;

        public StartStopCommand(Action<T> methodToExecute, Predicate<T> canExecuteEvaluator = null) {
            _action = methodToExecute ??
                      throw new ArgumentNullException(nameof(methodToExecute), @"Delegate commands can not be null");
            _canExecute = canExecuteEvaluator;
        }

#pragma warning disable 0067
        public event EventHandler CanExecuteChanged = delegate { };
#pragma warning restore 0067

        public bool CanExecute(object parameter) {
            return _canExecute == null || _canExecute((T) parameter);
        }

        public void Execute(object parameter) {
            _action((T) parameter);
        }
    }
}