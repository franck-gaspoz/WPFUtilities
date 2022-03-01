using Microsoft.Extensions.Hosting;

using WPFUtilities.Components.ServiceComponent;

namespace SampleApp.Components.Settings
{
    public class SettingsComponent :
        AbstractServiceComponent,
        IServiceComponent
    {
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
