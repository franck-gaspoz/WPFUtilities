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
        /// <inheritdoc/>
        public override void ConfigureServices(
            HostBuilderContext hostBuilderContext,
            IServiceComponentCollection services
        )
        {
            // main window dependencies

            services.AddSingleton<MainWindow>()
                    .AddSingleton<IMainWindowViewModel, MainWindowViewModel>();

            // log view model component

            services.AddSingleton<LogComponent>();
        }
    }
}
