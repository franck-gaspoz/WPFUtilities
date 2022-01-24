
using System.Linq;

using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

using WPFUtilities.Components.Component;
using WPFUtilities.Components.Logging.ListLogger;

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
            var loggerProvider = ComponentHost.Services.GetServices<ILoggerProvider>()
                .OfType<ListLoggerProvider>()
                .FirstOrDefault();
            if (loggerProvider != null)
            {
                var loggerConfig = loggerProvider.GetCurrentConfig();
            }
        }
    }
}
