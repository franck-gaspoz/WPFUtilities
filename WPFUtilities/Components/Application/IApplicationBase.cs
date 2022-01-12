namespace WPFUtilities.Components.Application
{
    /// <summary>
    /// application facade base
    /// </summary>
    public interface IApplicationBase
    {
        /// <summary>
        /// application view model
        /// </summary>
        IApplicationViewModelBase ViewModelBase { get; set; }
    }
}
