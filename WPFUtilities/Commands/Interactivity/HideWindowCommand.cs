using System;
using System.Windows;

namespace WPFUtilities.Commands.Interactivity
{
    /// <summary>
    /// hide a window
    /// </summary>
    public class HideWindowCommand
        : AbstractCommand<HideWindowCommand>
    {
        /// <inheritdoc/>
        public override bool CanExecute(object parameter) => true;

        /// <summary>
        /// hide the window
        /// </summary>
        /// <param name="parameter">a window instance or a Func&lt;Window&gt;</param>
        public override void Execute(object parameter)
        {
            if (parameter is Window w
                && w != null)
                w.Hide();
            else
            {
                var a = (Func<Window>)parameter;
                var win = a?.Invoke();
                win.Hide();
            }
        }
    }
}
