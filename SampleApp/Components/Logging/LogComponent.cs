﻿using Microsoft.Extensions.Hosting;

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
            IServiceComponentCollection services)
        {
            services.AddSingleton<ILogViewModel, LogViewModel>();
            new LogViewModelServiceDependencyInitializer()
                .ConfigureServices(
                    context,
                    services.Services
                );
        }
    }
}