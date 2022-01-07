using System.Windows;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

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
                InitializeHost = (hostBuilder) => InitializeHost(hostBuilder),
                ConfigureServices = (services) => ConfigureServices(services),
                InitializeMainWindow = (window) => InitializeWindow(window),
                ShutdownAction = () => ShutdownAction()
            };
        }

        /// <summary>
        /// initialize application [step 1]
        /// </summary>
        void Initialize()
        {

        }

        /// <summary>
        /// intialize host [step 2]
        /// </summary>
        /// <param name="hostBuilder"></param>
        void InitializeHost(IHostBuilder hostBuilder)
        {

        }

        /// <summary>
        /// configure services (DI) [step 3]
        /// </summary>
        /// <param name="services">services</param>
        void ConfigureServices(IServiceCollection services)
        {

        }

        /// <summary>
        /// initialize main window [step 4]
        /// </summary>
        /// <param name="window"></param>
        void InitializeWindow(Window window)
        {

        }

        /// <summary>
        /// shtudown action
        /// </summary>
        void ShutdownAction()
        {

        }
    }
}
