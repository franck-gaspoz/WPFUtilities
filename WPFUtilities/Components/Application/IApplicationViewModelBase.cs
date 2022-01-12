using WPFUtilities.ComponentModels;

namespace WPFUtilities.Components.Application
{
    /// <summary>
    /// application view model
    /// </summary>
    public interface IApplicationViewModelBase : IModelBase
    {
        /// <summary>
        /// indicates if app is busy or not. when buzy, any standard ui command may not be executable
        /// </summary>
        bool IsBuzy { get; set; }
    }
}
