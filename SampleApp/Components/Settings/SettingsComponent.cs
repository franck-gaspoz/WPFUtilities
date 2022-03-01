using Microsoft.Extensions.Hosting;

using WPFUtilities.Components.ServiceComponent;

namespace SampleApp.Components.Settings
{
    //[ServiceDependency]
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
                .AddSingleton<IDataViewModel, DataViewModel>()
                .AddSingleton<DataProviderMediator>();
        }

        /// <inheritdoc/>
        public override void Build()
        {
            base.Build();
            _ = this.ComponentHost.Services.GetRequiredService<IDataViewModel>();
            _ = this.ComponentHost.Services.GetRequiredService<DataProviderMediator>();
        }
    }
}
