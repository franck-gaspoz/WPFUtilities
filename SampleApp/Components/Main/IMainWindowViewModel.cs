
using WPFUtilities.ComponentModels;

namespace SampleApp.Components.Main
{
    /// <summary>
    /// main window view model
    /// </summary>
    public interface IMainWindowViewModel : IModelBase
    {
        /// <summary>
        /// main window title
        /// </summary>
        string Title { get; }

        /// <summary>
        /// window number
        /// </summary>
        int Number { get; set; }
    }
}
