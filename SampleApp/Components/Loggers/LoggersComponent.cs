using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using SampleApp.Components.Data.KeyValue;
using SampleApp.Components.Hosts;
using SampleApp.Components.Loggers.Mediators;

using WPFUtilities.Components.Logging.ListLogger;
using WPFUtilities.Components.ServiceComponent;

namespace SampleApp.Components.Loggers
{
    public class LoggersComponent :
        AbstractServiceComponent,
        IServiceComponent
    {
        IListLoggerModel _listLoggerModel;

        public LoggersComponent(IListLoggerModel listLoggerModel)
        {
            _listLoggerModel = listLoggerModel;
        }

        /// <inheritdoc/>
        public override void ConfigureServices(
            HostBuilderContext context,
            IServiceComponentCollection services)
        {
            services
                .AddSingleton<ILoggersViewModel, LoggersViewModel>()
                .AddSingleton<IKeyValueViewModel, KeyValueViewModel>()
                .AddSingleton<DataProviderMediator>()
                .AddSingleton<HostsComponent>();

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
            _ = this.ComponentHost.Services.GetRequiredService<DataProviderMediator>();
        }
    }
}
