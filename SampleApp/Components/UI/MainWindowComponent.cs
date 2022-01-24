using Microsoft.Extensions.Hosting;

using SampleApp.Components.Logging;

using WPFUtilities.Components.Component;

namespace SampleApp.Components.UI
{
    /// <summary>
    /// UI component
    /// </summary>
    public class MainWindowComponent :
        AbstractServiceComponent,
        IServiceComponent
    {
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

                    .AddSingleton<LogComponent>();
        }
    }
}
