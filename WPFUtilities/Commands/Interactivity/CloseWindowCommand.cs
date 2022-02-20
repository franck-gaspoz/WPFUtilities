using System;
using System.Windows;

using WPFUtilities.Commands.Abstract;

namespace WPFUtilities.Commands.Interactivity
{
    /// <summary>
    /// close a window
    /// </summary>
    [Obsolete]
    public class CloseWindowCommand
        : AbstractCommand<CloseWindowCommand>
    {
        /// <inheritdoc/>
        public override bool CanExecute(object parameter) => true;

        /// <summary>
        /// close the window
        /// </summary>
        /// <param name="parameter">a window instance or a Func&lt;Window&gt;</param>
        public override void Execute(object parameter)
        {
            if (parameter is Window win
                && win != null)
                win.Close();
            else
            {
                var a = (Func<Window>)parameter;
                var w = a?.Invoke();
                w.Close();
            }
        }
    }
}