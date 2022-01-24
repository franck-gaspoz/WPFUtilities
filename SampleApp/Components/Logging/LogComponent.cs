using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

using WPFUtilities.Components.Component;

namespace SampleApp.Components.Logging
{
    /// <summary>
    /// log component
    /// </summary>
    public class LogComponent :
        AbstractServiceComponent,
        IServiceComponent
    {
        /// <inheritdoc/>
        public override void ConfigureServices(
            HostBuilderContext context,
            IServiceComponentCollection services)
        {
            services.AddSingleton<ILogViewModel, LogViewModel>();

            /*new LogViewModelServiceDependencyInitializer()
                .ConfigureServices(
                    context,
                    services.Services
                );*/
        }

        /// <inheritdoc/>
        public override void Build()
        {
            base.Build();
            // need to get the list logger provider
            var loggers = ComponentHost.RootHost.Services.GetServices<ILoggerProvider>();
        }
    }
}
