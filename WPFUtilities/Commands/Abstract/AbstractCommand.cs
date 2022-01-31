using System;
using System.Windows.Input;

using WPFUtilities.ComponentModels;

using syswin = System.Windows;

namespace WPFUtilities.Commands.Abstract
{
    /// <summary>
    /// abstract command singleton service
    /// </summary>
    public abstract class AbstractCommand<ImplType> :
        SingletonService<ImplType>, ICommand
        where ImplType : ICommand
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
            => syswin.Application.Current != null && syswin.Application.Current.MainWindow != null;

        /// <summary>
        /// execute command
        /// </summary>
        /// <param name="parameter">parameter</param>
        public abstract void Execute(object parameter);
    }
}