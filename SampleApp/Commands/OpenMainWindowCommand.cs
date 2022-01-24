using System.Windows;
using System.Windows.Input;

using SampleApp.Components.UI;

using WPFUtilities.Commands;
using WPFUtilities.Components.Component;
using WPFUtilities.Extensions.App;

namespace SampleApp.Commands
{
    /// <summary>
    /// open a main window
    /// </summary>
    public class OpenMainWindowCommand : AbstractCommand<OpenMainWindowCommand>, ICommand
    {
        /// <inheritdoc/>
        public override bool CanExecute(object parameter)
            => true;

        /// <summary>
        /// open a main window
        /// </summary>
        /// <param name="parameter">the window that launched the command : a dependency object with ComponentHost attached property</param>
        public override void Execute(object parameter)
        {
            if (parameter is DependencyObject dependencyObject)
            {
                var componentHost = (IComponentHost)dependencyObject.GetValue(AttachedProperties.ComponentHostProperty);
                if (componentHost != null)
                {
                    var application = this.GetApplication();
                    var mainWindowComponent = new MainWindowComponent();
                    mainWindowComponent.ConfigureServices();
                    mainWindowComponent.Build();
                    var window = (Window)mainWindowComponent.ComponentHost.Services.GetService(application.ApplicationBaseSettings.MainWindowType);
                    window.Show();
                }
            }
        }
    }
}