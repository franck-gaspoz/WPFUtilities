
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using SampleApp.Components.Main;

using WPFUtilities.Components.Application;
using WPFUtilities.Components.Logging.ListLogger;

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
        /// creates a new instance, starts the app, build and display window
        /// </summary>
        public App() : base(
            new ApplicationBaseSettings
            {
                MainWindowType = typeof(MainWindow),
                MainWindowComponentInterfaceType = typeof(IMainWindowComponent),
                MainWindowComponentImplementationType = typeof(MainWindowComponent),
            })
        { }

        /// <summary>
        /// add additional application host initializations
        /// </summary>
        /// <param name="context">host builder context</param>
        /// <param name="services">services</param>
        public override void ConfigureServices(HostBuilderContext context, IServiceCollection services)
        {
            base.ConfigureServices(context, services);

            // add list logger

            var listLoggerModel = new ListLoggerModel();
            services.AddSingleton<IListLoggerModel>((sp) => listLoggerModel);
            services.AddLogging((loggingBuilder) =>
            {
                loggingBuilder.AddListLogger(
                    (listLoggerConfiguration) =>
                    {
                        listLoggerConfiguration.Target = listLoggerModel.LogItems;
                    });
            });
        }

    }
}
