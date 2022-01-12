
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
        ServiceComponent,
        IServiceComponent
    {
        /// <summary>
        /// dependencies needed by the component
        /// <para>declares component scope depdendencies</para>
        /// </summary>
        /// <param name="hostBuilderContext">host builder context</param>
        /// <param name="serviceCollection">services</param>
        public override void ConfigureServices(
            HostBuilderContext hostBuilderContext,
            IServiceCollection services
        )
        {
            services.AddSingleton<MainWindow>();
            services.AddSingleton<IMainWindowViewModel, MainWindowViewModel>();
            services.AddSingleton<ILogViewModel, LogViewModel>();
            new LogViewModelServiceDependencyInitializer()
                .ConfigureServices(
                    hostBuilderContext,
                    services
                );
        }
    }
}
