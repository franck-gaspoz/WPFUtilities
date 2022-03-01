using WPFUtilities.ComponentModels;

namespace SampleApp.Components.Settings
{
    public interface ISettingsFileViewModel : IModelBase
    {
        /// <summary>
        /// is visible
        /// </summary>
        bool IsVisible { get; set; }
    }
}
