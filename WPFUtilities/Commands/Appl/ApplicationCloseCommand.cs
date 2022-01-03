using System.Windows;

namespace WPFUtilities.Commands.Appl
{
    /// <summary>
    /// close application command (close main window)
    /// </summary>
    public class ApplicationCloseCommand : AbstractCommand<ApplicationCloseCommand>
    {
        /// <inheritdoc/>
        public override void Execute(object parameter)
            => Application.Current.MainWindow.Close();
    }
}
