using System;

using Microsoft.Extensions.DependencyInjection;

namespace WPFUtilities.Components.Component
{
    /// <summary>
    /// collection of services components
    /// </summary>
    public sealed class ServiceComponentCollection : IServiceComponentCollection
    {
        readonly IServiceCollection _services;

        /// <summary>
        /// build a new instance
        /// </summary>
        public ServiceComponentCollection(IServiceCollection services)
        {
            _services = services;
        }

        /// <inheritdoc/>
        public IServiceComponentCollection AddSingleton<TService>() where TService : class
        {
            _services.AddSingleton<TService>();
            return this;
        }

        /// <inheritdoc/>
        public IServiceComponentCollection AddSingleton(Type tservice)
        {
            _services.AddSingleton(tservice);
            return this;
        }

        /// <inheritdoc/>
        public IServiceComponentCollection AddTransient<TService>() where TService : class
        {
            _services.AddTransient<TService>();
            return this;
        }

        /// <inheritdoc/>
        public IServiceComponentCollection AddTransient(Type tservice)
        {
            _services.AddTransient(tservice);
            return this;
        }
    }
}
