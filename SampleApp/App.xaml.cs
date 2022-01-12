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
    public partial class App :
        ApplicationBase,
        IApplicationBase
    {
        /// <summary>
        /// creates a new instance, start the app, build and display window
        /// </summary>
        public App()
        {
            ApplicationBaseSettings = new ApplicationBaseSettings
            {
                MainWindowType = typeof(MainWindow),
                MainWindowComponentType = typeof(MainWindowComponent)
            };
        }

        /// <summary>
        /// initialize application [step 1]
        /// </summary>
        protected override void Initialize()
        {
            base.Initialize();
        }

        /// <summary>
        /// configure component [step 2]
        /// </summary>
        /// <param name="services">services</param>
        public override void Configure(HostBuilderContext context, IServiceCollection services)
        {
            base.Configure(context, services);
        }

        /// <summary>
        /// initialize main window [step 5]
        /// </summary>
        /// <param name="mainWindow">main window</param>
        protected override void InitializeMainWindow(Window mainWindow)
        {
            base.InitializeMainWindow(mainWindow);
        }

        /// <summary>
        /// on start ui
        /// </summary>
        protected override void OnStartUI()
        {
            base.OnStartUI();
        }

        /// <summary>
        /// on shtudown
        /// </summary>
        protected override void OnShutdown()
        {

        }
    }
}
