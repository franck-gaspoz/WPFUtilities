namespace WPFUtilities.Components.Component
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
    }
}
