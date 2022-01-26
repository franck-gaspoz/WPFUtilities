namespace WPFUtilities.Components.ServiceComponent
{
    /// <summary>
    /// a service type (DI) component
    /// </summary>
    public interface IServiceComponent
    {
        /// <inheritdoc/>
        IComponentHost ComponentHost { get; }

        /// <summary>
        /// configure services dependencies for owned host
        /// </summary>
        void Configure();

        /// <summary>
        /// build the owned host
        /// </summary>
        void Build();

        /// <summary>
        /// indicates if the component has been built
        /// </summary>
        /// <returns>true if the component is built, false otherwize</returns>
        bool IsBuilt { get; }
    }
}
