using System.Windows.Input;

using WPFUtilities.Commands;

namespace SampleApp.Commands
{
    /// <summary>
    /// on application init command callback
    /// </summary>
    public class OnMainWindowShownCommand : AbstractCommand<OnMainWindowShownCommand>, ICommand
    {
        /// <inheritdoc/>
        public override bool CanExecute(object parameter)
            => true;

        /// <summary>
        /// on application init command callback
        /// </summary>
        /// <param name="parameter">not used</param>
        public override void Execute(object parameter)
        {
            // ...
        }
    }
}