
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using SampleApp.Components.Logging;

using WPFUtilities.Components.Component;

namespace SampleApp.Components.UI
{
    /// <summary>
    /// UI component
    /// </summary>
    public class MainWindowComponent : ServiceComponent
    {
        /// <summary>
        /// create a new component instance
        /// <para>can get services dependencies trought constructor</para>
        /// <para>get parent scope dependencies</para>
        /// </summary>
        public MainWindowComponent()
        {

        }

        /// <summary>
        /// dependencies needed by the component
        /// <para>declares component scope depdendencies</para>
        /// </summary>
        /// <param name="hostBuilderContext">host builder context</param>
        /// <param name="serviceCollection">services</param>
        public override void Configure(
            HostBuilderContext hostBuilderContext,
            IServiceCollection services
        )
        {
            services.AddSingleton<MainWindow>();
            services.AddSingleton<IMainWindowViewModel, MainWindowViewModel>();
            services.AddSingleton<ILogViewModel, LogViewModel>();
            new LogViewModelServiceDependencyInitializer()
                .Configure(
                    hostBuilderContext,
                    services
                );
        }
    }
}
