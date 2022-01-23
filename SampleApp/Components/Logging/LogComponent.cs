
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using WPFUtilities.Components.Component;

namespace SampleApp.Components.Logging
{
    public class LogComponent :
        AbstractServiceComponent,
        IServiceComponent
    {
        /// <inheritdoc/>
        public override void ConfigureServices(
            HostBuilderContext context,
            IServiceCollection services)
        {
            services.AddSingleton<ILogViewModel, LogViewModel>();
            new LogViewModelServiceDependencyInitializer()
                .ConfigureServices(
                    context,
                    services
                );
        }
    }
}
