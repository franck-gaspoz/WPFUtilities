using System;

using Microsoft.Extensions.DependencyInjection;

namespace WPFUtilities.Components.ServiceComponent
{
    /// <summary>
    /// collection of services components
    /// </summary>
    public sealed class ServiceComponentCollection : IServiceComponentCollection
    {
        #region properties

        /// <inheritdoc/>
        public IServiceCollection Services { get; }

        /// <inheritdoc/>
        public IComponentHost ComponentHost { get; }

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
            ComponentHost = componentHost;
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

#if no
        #region components registration

        /// <inheritdoc/>
        public IServiceComponentCollection AddSingletonComponent<TService>() where TService : class
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public IServiceComponentCollection AddTransientComponent<TService>() where TService : class
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public IServiceComponentCollection AddSingletonComponent<TService, TImplementation>()
            where TService : class
            where TImplementation : class, TService
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public IServiceComponentCollection AddTransientComponent<TService, TImplementation>()
            where TService : class
            where TImplementation : class, TService
        {
            throw new NotImplementedException();
        }

        #endregion
#endif
    }
}
