using WPFUtilities.Commands.Abstract;

using syswin = System.Windows;

namespace WPFUtilities.Commands.Application
{
    /// <summary>
    /// close application command (close main window)
    /// </summary>
    public class CloseApplicationCommand : AbstractCommand<CloseApplicationCommand>
    {
        /// <inheritdoc/>
        public override void Execute(object parameter)
            => syswin.Application.Current.MainWindow.Close();
    }
}
