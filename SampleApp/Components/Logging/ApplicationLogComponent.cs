
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using WPFUtilities.Components.Logging.ListLogger;
using WPFUtilities.Components.ServiceComponent;

namespace SampleApp.Components.Logging
{
    /// <summary>
    /// application log component
    /// </summary>
    public class ApplicationLogComponent :
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
            // there is no inheritance from components hosts when DI creates a service
            // thus we have here to bring the model to the local scope
            services.Services.AddSingleton<IListLoggerModel>(
                (sp) => ComponentHost.RootHost.Services.GetService<IListLoggerModel>());
        }

        /// <inheritdoc/>
        public override void Build()
        {
            base.Build();
            // instantiate the log model mediator
            ComponentHost.Services.GetService<LogModelMediator>();
        }
    }
}
