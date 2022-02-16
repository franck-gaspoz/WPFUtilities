using System.Windows;
using System.Windows.Input;

using WPFUtilities.Commands.Abstract;

namespace WPFUtilities.Commands.Application
{
    /// <summary>
    /// close a window
    /// </summary>
    public class HideWindowCommand : AbstractCommand<LogCommand>, ICommand
    {
        /// <inheritdoc/>
        public override bool CanExecute(object parameter)
            => true;

        /// <summary>
        /// hide a window
        /// </summary>
        /// <param name="window">window</param>
        public override void Execute(object window)
            => (window as Window)?.Hide();
    }
}
