using System;
using System.Windows.Input;

namespace ViewModels.Auxiliary
{
    /// <summary>
    /// The ViewModelCommand class - an ICommand that can fire a function.
    /// </summary>
    public class Command : ICommand
    {

        protected Action _action = null;
        protected Action<object> _parameterizedAction = null;

        private Predicate<object> _canExecute;

        private bool canExecute = true;

        /// <summary>
        /// Initializes a new instance of the <see cref="Command"/> class.
        /// </summary>
        /// <param name="action">Function to execute when command is called</param>
        public Command(Action<object> action)
        {
            _parameterizedAction = action;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Command"/> class.
        /// </summary>
        /// <param name="action">Function to execute when command is called</param>
        public Command(Action action)
        {
            _action = action;
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="Command"/> class.
        /// </summary>
        /// <param name="action">Function to execute when command is called</param>
        /// <param name="canExecute">Function which returns if command can be executed</param>
        public Command(Action action, Predicate<object> canExecute)
        {
            _action = action;
            _canExecute = canExecute;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Command"/> class.
        /// </summary>
        /// <param name="parameterizedAction">Parameterized function to execute when command is called</param>
        /// <param name="canExecute">Function which returns if command can be executed</param>
        public Command(Action<object> parameterizedAction, Predicate<object> canExecute)
        {
            _parameterizedAction = parameterizedAction;
            _canExecute = canExecute;
        }

        /// <summary>
        /// Defines the method that determines whether the command can execute in its current state.
        /// </summary>
        /// <param name="parameter">Data used by the command.
        ///  If the command does not require data to be passed,
        ///  this object can be set to null.</param>
        /// <returns>
        /// true if this command can be executed; otherwise, false.
        /// </returns>
        bool ICommand.CanExecute(object parameter)
        {
            if (_canExecute == null)
                return true;
            if (canExecute == _canExecute(parameter))
                return canExecute;
            canExecute = _canExecute(parameter);
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
            return canExecute;
        }

        /// <summary>
        /// Defines the method to be called when the command is invoked.
        /// </summary>
        /// <param name="parameter">Data used by the command.
        ///  If the command does not require data to be passed,
        ///  this object can be set to null.</param>
        void ICommand.Execute(object parameter)
        {
            if (_action != null)
                _action();
            else _parameterizedAction?.Invoke(parameter);
        }

        /// <summary>
        /// Occurs when can execute is changed.
        /// </summary>
        public event EventHandler CanExecuteChanged;
    }
}
