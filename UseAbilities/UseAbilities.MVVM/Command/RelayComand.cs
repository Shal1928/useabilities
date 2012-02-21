using System;
using System.Diagnostics;
using System.Windows.Input;

namespace Common.Code.UseAbilities.MVVM.Command
{
    public class RelayCommand<T> : ICommand
    {
        private readonly Action<T> _execute;
        private readonly Predicate<T> _canExecute;

        public RelayCommand(Action<T> execute) : this(execute, null)
        {

        }

        public RelayCommand(Action<T> execute, Predicate<T> canExecute)
        {
            if (execute == null) throw new ArgumentNullException("execute");

            _execute = execute;
            _canExecute = canExecute;
        }

        [DebuggerStepThrough]
        public bool CanExecute(T parameter)
        {
            return _canExecute == null || _canExecute(parameter);
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public void Execute(T parameter)
        {
            _execute(parameter);
        }

        #region Implementation ICommand

        public bool CanExecute(object parameter)
        {
            return CanExecute((T)parameter);
        }

        public void Execute(object parameter)
        {
            _execute((T)parameter);
        }

        #endregion
    }

    public class RelayCommand : RelayCommand<object>
    {
        public RelayCommand(Action<object> execute): this(execute, canExecute: null)
        {
            //
        }

        public RelayCommand(Action<object> execute, Predicate<object> canExecute): base(execute, canExecute)
        {
            //
        }
    }
}
