using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using SampleApp.Commands;
using SampleApp.Components.Logging;

using WPFUtilities.Commands.Application;
using WPFUtilities.Components.Logging.ListLogger;
using WPFUtilities.Components.ServiceComponent;

namespace SampleApp.Components.UI
{
    /// <summary>
    /// UI component
    /// </summary>
    public sealed class MainWindowComponent :
        AbstractServiceComponent,
        IMainWindowComponent,
        IServiceComponent
    {
        /// <inheritdoc/>
        public IListLoggerModel ListLoggerModel { get; private set; }

        /// <summary>
        /// build a new instance
        /// </summary>
        public MainWindowComponent() { }

        /// <summary>
        /// build a new instance that inherits from a parent context
        /// </summary>
        /// <param name="parentHost">parent host</param>
        public MainWindowComponent(IComponentHost parentHost)
        {
            ComponentHost.ParentHost = parentHost;
        }

        /// <inheritdoc/>
        public override void ConfigureServices(
            HostBuilderContext hostBuilderContext,
            IServiceComponentCollection services
        )
        {
            // main window dependencies

            services.AddSingleton<MainWindow>()
                    .AddSingleton<IMainWindowViewModel, MainWindowViewModel>()

            // log component

                    .AddSingleton<ApplicationLogComponent>()
                    .AddSingleton<WindowLogComponent>();

            // 'window' scopped logger

            ListLoggerModel = new ListLoggerModel();
            services.Services.AddSingleton<IListLoggerModel>((sp) => ListLoggerModel);
            services.Services.AddLogging((loggingBuilder) =>
            {
                loggingBuilder.AddListLogger((config) =>
                    config.Target = ListLoggerModel.LogItems);
            });

            // window scope (component scope) commands [app logic]

            services.Services
                .AddSingleton<LogCommand>()
                .AddSingleton<ClearLogCommand>()
                .AddSingleton<LogObjectCommand>()
                .AddSingleton<OnMainWindowShownCommand>();
        }
    }
}
