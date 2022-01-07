using System.Windows;

using Microsoft.Extensions.DependencyInjection;

using SampleApp.Components.UI;

using WPFUtilities.Components.Appl;

namespace SampleApp
{
    /// <summary>
    /// Application interactor
    /// </summary>
    public partial class App : ApplicationBase
    {
        /// <summary>
        /// creates a new instance, start the app, build and display window
        /// </summary>
        public App()
        {
            ApplicationBaseSettings = new ApplicationBaseSettings
            {
                MainWindowType = typeof(MainWindow),
                Initialize = () => Initialize(),
                InitializeWindow = (window) => InitializeWindow(window),
                ConfigureServices = (services) => ConfigureServices(services)
            };
            //Start(settings);
        }

        void ConfigureServices(IServiceCollection services)
        {

        }

        void Initialize()
        {

        }

        void InitializeWindow(Window window)
        {

        }
    }
}
