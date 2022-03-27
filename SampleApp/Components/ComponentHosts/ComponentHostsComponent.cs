using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using SampleApp.Components.ComponentHosts.Hosts;
using SampleApp.Components.ComponentHosts.Loggers;
using SampleApp.Components.ComponentHosts.Mediators;
using SampleApp.Components.ComponentHosts.Services;

using WPFUtilities.Components.Logging.ListLogger;
using WPFUtilities.Components.ServiceComponent;

namespace SampleApp.Components.ComponentHosts
{
    public class ComponentHostsComponent :
        AbstractServiceComponent,
        IServiceComponent
    {
        IListLoggerModel _listLoggerModel;

        public ComponentHostsComponent(IListLoggerModel listLoggerModel)
        {
            _listLoggerModel = listLoggerModel;
        }

        /// <inheritdoc/>
        public override void ConfigureServices(
            HostBuilderContext context,
            IServiceComponentCollection services)
        {
            services
                .AddSingleton<IComponentHostsViewModel, ComponentHostsViewModel>()
                .AddSingleton<HostsComponent>()

                .AddSingleton<ILoggersViewModel, LoggersViewModel>()
                .AddSingleton<IServicesViewModel, ServicesViewModel>()
                .AddSingleton<IServicesRegisteredViewModel, ServicesRegisteredViewModel>()
                .AddSingleton<HostLoggersMediator>()
                .AddSingleton<HostServicesMediator>()
                .AddSingleton<HostServicesRegisteredMediator>()
                ;

            services.Services
                .AddSingleton<IListLoggerModel>(
                    (sp) => _listLoggerModel);

            // enable logging to parent list logger model
            // TODO: inherits from parent host loggers
            services.Services.AddLogging((loggingBuilder) =>
            {
                loggingBuilder.AddListLogger(
                    (listLoggerConfiguration) =>
                    {
                        listLoggerConfiguration.Target = _listLoggerModel.LogItems;
                    });
            });
        }

        /// <inheritdoc/>
        public override void Build()
        {
            base.Build();
            _ = this.ComponentHost.Services.GetRequiredService<HostLoggersMediator>();
            _ = this.ComponentHost.Services.GetRequiredService<HostServicesMediator>();
            _ = this.ComponentHost.Services.GetRequiredService<HostServicesRegisteredMediator>();
        }
    }
}
