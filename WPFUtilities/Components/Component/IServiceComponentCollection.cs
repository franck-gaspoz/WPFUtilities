using System;

namespace WPFUtilities.Components.Component
{
    /// <summary>
    /// collection of services components
    /// </summary>
    public interface IServiceComponentCollection
    {
        /// <summary>
        /// add a singleton service of the type specified in TService
        /// </summary>
        /// <typeparam name="TService">service type</typeparam>
        /// <returns>services compponent collection</returns>
        IServiceComponentCollection AddSingleton<TService>() where TService : class;

        /// <summary>
        /// add a singleton service of the type specified in TService
        /// </summary>
        /// <param name="tservice">type of the service</param>
        /// <returns>services compponent collection</returns>
        IServiceComponentCollection AddSingleton(Type tservice);

        /// <summary>
        /// add a transient service of the type specified in TService
        /// </summary>
        /// <typeparam name="TService">service type</typeparam>
        /// <returns>services compponent collection</returns>
        IServiceComponentCollection AddTransient<TService>() where TService : class;

        /// <summary>
        /// add a transient service of the type specified in TService
        /// </summary>
        /// <param name="tservice">type of the service</param>
        /// <returns>services compponent collection</returns>
        IServiceComponentCollection AddTransient(Type tservice);
    }
}
