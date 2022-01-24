using System;

using Microsoft.Extensions.DependencyInjection;

namespace WPFUtilities.Components.Component
{
    /// <summary>
    /// collection of services components
    /// </summary>
    public sealed class ServiceComponentCollection : IServiceComponentCollection
    {
        /// <inheritdoc/>
        public IServiceCollection Services { get; }

        /// <summary>
        /// build a new instance
        /// </summary>
        public ServiceComponentCollection(IServiceCollection services)
        {
            Services = services;
        }

        /// <inheritdoc/>
        public IServiceComponentCollection AddSingleton<TService>() where TService : class
        {
            Services.AddSingleton<TService>();
            return this;
        }

        /// <inheritdoc/>
        public IServiceComponentCollection AddSingleton(Type tservice)
        {
            Services.AddSingleton(tservice);
            return this;
        }

        /// <inheritdoc/>
        public IServiceComponentCollection AddSingleton<TService, TImplementation>()
            where TService : class
            where TImplementation : class, TService
        {
            Services.AddSingleton<TService, TImplementation>();
            return this;
        }

        /// <inheritdoc/>
        public IServiceComponentCollection AddSingleton(Type tservice, Type timplementation)
        {
            Services.AddSingleton(tservice, timplementation);
            return this;
        }

        /// <inheritdoc/>
        public IServiceComponentCollection AddTransient<TService>() where TService : class
        {
            Services.AddTransient<TService>();
            return this;
        }

        /// <inheritdoc/>
        public IServiceComponentCollection AddTransient(Type tservice)
        {
            Services.AddTransient(tservice);
            return this;
        }

        /// <inheritdoc/>
        public IServiceComponentCollection AddTransient<TService, TImplementation>()
            where TService : class
            where TImplementation : class, TService
        {
            Services.AddTransient<TService, TImplementation>();
            return this;
        }

        /// <inheritdoc/>
        public IServiceComponentCollection AddTransient(Type tservice, Type timplementation)
        {
            Services.AddTransient(tservice, timplementation);
            return this;
        }
    }
}
