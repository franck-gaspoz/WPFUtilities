namespace WPFUtilities.Components.Appl
{
    /// <summary>
    /// application facade base
    /// </summary>
    public interface IApplicationBase
    {
        /// <summary>
        /// application view model
        /// </summary>
        IAppViewModelBase ViewModelBase { get; set; }
    }
}
