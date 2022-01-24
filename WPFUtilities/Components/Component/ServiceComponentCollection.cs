using System;

using Microsoft.Extensions.DependencyInjection;

namespace WPFUtilities.Components.Component
{
    /// <summary>
    /// collection of services components
    /// </summary>
    public sealed class ServiceComponentCollection : IServiceComponentCollection
    {
        #region properties

        /// <inheritdoc/>
        public IServiceCollection Services { get; }

        readonly IComponentHost _componentHost;

        #endregion

        #region build & init

        /// <summary>
        /// build a new instance
        /// </summary>
        /// <param name="componentHost">component host that owns the service component collection</param>
        /// <param name="services">the wrapped host services collection</param>
        public ServiceComponentCollection(
            IComponentHost componentHost,
            IServiceCollection services)
        {
            Services = services;
            _componentHost = componentHost;
        }

        #endregion

        #region services registration

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

        #endregion

        #region components registration

        /// <summary>
        /// add a multiton service component of the type specified in TService, identified by a component id
        /// </summary>
        /// <typeparam name="TService">service type</typeparam>
        /// <param name="componentId">component identifier</param>
        /// <returns>services component collection</returns>
        public IServiceComponentCollection AddComponent<TService>(string componentId) where TService : class
        {
            //_componentHost.ServiceComponentRegister.AddComponent();
            Services.AddSingleton<TService>((serviceProvider) =>
            {
                return (TService)null;
            });
            return this;
        }

        #endregion
    }
}
