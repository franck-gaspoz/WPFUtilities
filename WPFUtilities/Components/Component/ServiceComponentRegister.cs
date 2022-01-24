using System;
using System.Collections.Generic;

namespace WPFUtilities.Components.Component
{
    /// <summary>
    /// register of services components
    /// </summary>
    public sealed class ServiceComponentRegister
        : IServiceComponentRegister
    {
        /// <summary>
        /// components with an identifier (multitons)
        /// </summary>
        readonly Dictionary<string, Type> _identifiedComponents = new Dictionary<string, Type>();

        /// <summary>
        /// add a multiton service component of the type specified in TService, identified by a component id
        /// </summary>
        /// <typeparam name="TService">service type</typeparam>
        /// <param name="componentId">component identifier</param>
        /// <returns>register of services components</returns>
        public IServiceComponentRegister AddComponent<TService>(string componentId) where TService : class, IServiceComponent
        {
            if (_identifiedComponents.ContainsKey(componentId))
                throw new InvalidOperationException($"a component with id: {componentId} is already registered");
            _identifiedComponents.Add(componentId, typeof(TService));
            return this;
        }
    }
}
