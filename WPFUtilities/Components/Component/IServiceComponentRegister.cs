namespace WPFUtilities.Components.Component
{
    /// <summary>
    /// register of services components
    /// </summary>
    public interface IServiceComponentRegister
    {
        /// <summary>
        /// add a multiton service component of the type specified in TService, identified by a component id
        /// </summary>
        /// <typeparam name="TService">service type</typeparam>
        /// <param name="componentId">component identifier</param>
        /// <returns>register of services components</returns>
        IServiceComponentRegister AddComponent<TService>(string componentId) where TService : class, IServiceComponent;
    }
}