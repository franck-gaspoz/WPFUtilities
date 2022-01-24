using System;

using Microsoft.Extensions.DependencyInjection;

namespace WPFUtilities.Components.Component
{
    /// <summary>
    /// collection of services components
    /// </summary>
    public interface IServiceComponentCollection
    {
        #region properties

        /// <summary>
        /// wrapped service collection
        /// </summary>
        IServiceCollection Services { get; }

        #endregion

        #region services registration

        /// <summary>
        /// add a singleton service of the type specified in TService
        /// </summary>
        /// <typeparam name="TService">service type</typeparam>
        /// <returns>services component collection</returns>
        IServiceComponentCollection AddSingleton<TService>() where TService : class;

        /// <summary>
        /// add a singleton service of the type specified in TService
        /// </summary>
        /// <param name="tservice">type of the service</param>
        /// <returns>services component collection</returns>
        IServiceComponentCollection AddSingleton(Type tservice);

        /// <summary>
        /// add a singleton service of the interface type specified in TService and implementation type specified in TImplementation
        /// </summary>
        /// <typeparam name="TService">service interface type</typeparam>
        /// <typeparam name="TImplementation">service implementation type</typeparam>
        /// <returns>services component collection</returns>
        IServiceComponentCollection AddSingleton<TService, TImplementation>()
            where TService : class
            where TImplementation : class, TService;

        /// <summary>
        /// add a singleton service of the interface type specified in tservice and the implementation type specified in timplementation
        /// </summary>
        /// <param name="tservice">type of the service interface</param>
        /// <param name="timplementation">type of the service implementation</param>
        /// <returns>services component collection</returns>
        IServiceComponentCollection AddSingleton(Type tservice, Type timplementation);

        /// <summary>
        /// add a transient service of the type specified in TService
        /// </summary>
        /// <typeparam name="TService">service type</typeparam>
        /// <returns>services component collection</returns>
        IServiceComponentCollection AddTransient<TService>() where TService : class;

        /// <summary>
        /// add a transient service of the type specified in TService
        /// </summary>
        /// <param name="tservice">type of the service</param>
        /// <returns>services component collection</returns>
        IServiceComponentCollection AddTransient(Type tservice);

        /// <summary>
        /// add a transient service of the interface type specified in TService and implementation type specified in TImplementation
        /// </summary>
        /// <typeparam name="TService">service interface type</typeparam>
        /// <typeparam name="TImplementation">service implementation type</typeparam>
        /// <returns>services component collection</returns>
        IServiceComponentCollection AddTransient<TService, TImplementation>()
            where TService : class
            where TImplementation : class, TService;

        /// <summary>
        /// add a transient service of the interface type specified in tservice and the implementation type specified in timplementation
        /// </summary>
        /// <param name="tservice">type of the service interface</param>
        /// <param name="timplementation">type of the service implementation</param>
        /// <returns>services component collection</returns>
        IServiceComponentCollection AddTransient(Type tservice, Type timplementation);

        #endregion

        #region components registration

        /// <summary>
        /// add a multiton service component of the type specified in TService, identified by a component id
        /// </summary>
        /// <typeparam name="TService">service type</typeparam>
        /// <param name="componentId">component identifier</param>
        /// <returns>services component collection</returns>
        IServiceComponentCollection AddComponent<TService>(string componentId) where TService : class;

        #endregion
    }
}
