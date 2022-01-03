
using WPFUtilities.ViewModels;

namespace WPFUtilities.Components
{
    /// <summary>
    /// application facade
    /// </summary>
    public interface IApp
    {
        /// <summary>
        /// application view model
        /// </summary>
        IAppViewModel ViewModel { get; set; }
    }
}
