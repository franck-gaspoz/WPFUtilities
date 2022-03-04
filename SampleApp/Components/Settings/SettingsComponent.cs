using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using SampleApp.Components.Settings.Data;
using SampleApp.Components.Settings.Files;
using SampleApp.Components.Settings.Mediators;

using WPFUtilities.Components.Logging.ListLogger;
using WPFUtilities.Components.ServiceComponent;

namespace SampleApp.Components.Settings
{
    public class SettingsComponent :
        AbstractServiceComponent,
        IServiceComponent
    {
        IListLoggerModel _listLoggerModel;

        public SettingsComponent(IListLoggerModel listLoggerModel)
        {
            _listLoggerModel = listLoggerModel;
        }

        /// <inheritdoc/>
        public override void ConfigureServices(
            HostBuilderContext context,
            IServiceComponentCollection services)
        {
            services
                .AddSingleton<ISettingsViewModel, SettingsViewModel>()
                .AddSingleton<ISettingsFileViewModel, SettingsFileViewModel>()
                .AddSingleton<IDataViewModel, DataViewModel>()
                .AddSingleton<DataProviderMediator>()
                .AddSingleton<SettingsFileMediator>();

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
            _ = this.ComponentHost.Services.GetRequiredService<IDataViewModel>();
            _ = this.ComponentHost.Services.GetRequiredService<DataProviderMediator>();
            _ = this.ComponentHost.Services.GetRequiredService<SettingsFileMediator>();
        }
    }
}
