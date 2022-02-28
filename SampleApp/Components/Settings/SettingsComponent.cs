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
            //services.Services.AddSingleton<ISettingsViewModel>(
            //    (sp) => ComponentHost.RootHost.Services.GetService<ISettingsViewModel>());
            services.AddSingleton<ISettingsViewModel, SettingsViewModel>();
        }

        /// <inheritdoc/>
        public override void Build()
        {
            base.Build();
        }
    }
}
