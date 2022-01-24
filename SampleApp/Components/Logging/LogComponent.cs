
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

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
            services.AddSingleton<LogModelMediator>();
            services.Services.AddSingleton<IListLoggerModel>(
                (sp) => ComponentHost.RootHost.Services.GetService<IListLoggerModel>());
        }

        public override void Build()
        {
            base.Build();
            // instantiate the log model mediator
            ComponentHost.Services.GetService<LogModelMediator>();
        }
    }
}
