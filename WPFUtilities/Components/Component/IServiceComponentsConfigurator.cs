namespace WPFUtilities.Components.Component
{
    /// <summary>
    /// services components configurator
    /// </summary>
    public interface IServiceComponentsConfigurator
    {
        /// <summary>
        /// configure host services
        /// </summary>
        /// <param name="services">services</param>
        void ConfigureServices(IServiceComponentCollection services);
    }
}
