﻿
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using WPFUtilities.Components.Logging.ListLogger;
using WPFUtilities.Components.ServiceComponent;

namespace SampleApp.Components.Logging
{
    /// <summary>
    /// window log component
    /// </summary>
    public class WindowLogComponent :
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
                (sp) => ComponentHost.ParentHost.Services.GetService<IListLoggerModel>());
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
