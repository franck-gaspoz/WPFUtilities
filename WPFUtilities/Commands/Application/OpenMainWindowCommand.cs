using System.Windows;
using System.Windows.Input;

using WPFUtilities.Commands.Abstract;
using WPFUtilities.Extensions.App;

namespace WPFUtilities.Commands.Application
{
    /// <summary>
    /// open a new main window from application base settings
    /// </summary>
    public class OpenMainWindowCommand : AbstractCommand<OpenMainWindowCommand>, ICommand
    {
        /// <summary>
        /// open a main window
        /// </summary>
        /// <param name="parameter">not used</param>
        public override void Execute(object parameter)
        {
            var application = this.GetApplication();
            var mainWindowComponent = application.ApplicationHost.Services.GetComponent(
                application.ApplicationBaseSettings.MainWindowComponentInterfaceType
                );
            var window = (Window)mainWindowComponent.ComponentHost.Services.GetService(
                application.ApplicationBaseSettings.MainWindowType);
            window.Show();
        }
    }
}