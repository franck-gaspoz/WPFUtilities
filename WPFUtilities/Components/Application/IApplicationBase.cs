namespace WPFUtilities.Components.Application
{
    /// <summary>
    /// application facade base
    /// </summary>
    public interface IApplicationBase
    {
        /// <inheritdoc/>
        IApplicationHost ApplicationHost { get; }

        /// <inheritdoc/>
        IApplicationViewModelBase ViewModelBase { get; set; }

        /// <inheritdoc/>
        IApplicationBaseSettings ApplicationBaseSettings { get; }
    }
}
