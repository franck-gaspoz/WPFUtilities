using System;
using System.Windows;
using System.Windows.Input;

using WPFUtilities.ComponentModel;

namespace WPFUtilities.Commands
{
    /// <summary>
    /// abstract command
    /// </summary>
    public abstract class AbstractCommand<ConcreteType> :
        Singleton<ConcreteType>, ICommand
        where ConcreteType : class, ICommand, new()
    {

        /// <summary>
        /// default can execute changed event handler
        /// </summary>
        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        /// <summary>
        /// can execute
        /// </summary>
        /// <param name="parameter">parameter</param>
        /// <returns>true if can execute, false otherwize</returns>
        public virtual bool CanExecute(object parameter)
            => Application.Current != null && Application.Current.MainWindow != null;

        /// <summary>
        /// execute command
        /// </summary>
        /// <param name="parameter">parameter</param>
        public abstract void Execute(object parameter);
    }
}