
using Microsoft.Extensions.DependencyInjection;
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
        static int MainWindowComponentCounter = 0;

        /// <inheritdoc/>
        public override void ConfigureServices(
            HostBuilderContext hostBuilderContext,
            IServiceCollection services
        )
        {
            // main window dependencies

            services.AddSingleton<MainWindow>();
            services.AddSingleton<IMainWindowViewModel, MainWindowViewModel>();

            // log view model component

            services.AddSingleton<LogComponent>();
        }

        /// <inheritdoc/>
        public override void Build()
        {
            base.Build();
            var mainWindowViewModel = ComponentHost.Services.GetService<IMainWindowViewModel>();
            MainWindowComponentCounter++;
            mainWindowViewModel.Number = MainWindowComponentCounter;
        }
    }
}
