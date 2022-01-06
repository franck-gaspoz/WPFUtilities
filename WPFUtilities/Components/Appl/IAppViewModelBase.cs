using WPFUtilities.ComponentModel;

namespace WPFUtilities.Components.Appl
{
    /// <summary>
    /// application view model
    /// </summary>
    public interface IAppViewModelBase : IModelBase
    {
        /// <summary>
        /// indicates if app is busy or not. when buzy, any standard ui command may not be executable
        /// </summary>
        bool IsBuzy { get; set; }
    }
}
