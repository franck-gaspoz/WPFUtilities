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


        /// <summary>
        /// callback triggered main app main window is first displayed (visible on screen)
        /// </summary>
        /// <param name="sender">sender</param>
        void NotifyMainWindowDisplayed(object sender);
    }
}
