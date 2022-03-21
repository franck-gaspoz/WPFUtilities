using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using SampleApp.Components.ComponentHosts.Hosts;
using SampleApp.Components.ComponentHosts.Loggers;
using SampleApp.Components.ComponentHosts.Mediators;
using SampleApp.Components.Data.KeyValue;

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
                .AddSingleton<IKeyValueViewModel, KeyValueViewModel>()
                .AddSingleton<HostsComponent>()

                .AddSingleton<ILoggersViewModel, LoggersViewModel>()
                .AddSingleton<LoggersMediator>()
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
            _ = this.ComponentHost.Services.GetRequiredService<IKeyValueViewModel>();
            _ = this.ComponentHost.Services.GetRequiredService<LoggersMediator>();
        }
    }
}
