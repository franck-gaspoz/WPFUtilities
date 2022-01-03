namespace WPFUtilities.ViewModels
{
    /// <summary>
    /// application view model
    /// </summary>
    public interface IAppBaseViewModel
    {
        /// <summary>
        /// indicates if app is busy or not. when buzy, any standard ui command should not be executable
        /// </summary>
        bool IsBuzy { get; set; }
    }
}
