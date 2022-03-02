using WPFUtilities.ComponentModels;

namespace SampleApp.Components.Settings
{
    public interface ISettingsFileViewModel : IModelBase
    {
        /// <summary>
        /// is visible
        /// </summary>
        bool IsVisible { get; set; }

        /// <summary>
        /// file text
        /// </summary>
        string FileText { get; set; }

        /// <summary>
        /// file path
        /// </summary>
        string FileName { get; set; }

        /// <summary>
        /// file name
        /// </summary>
        string FilePath { get; set; }

        /// <summary>
        /// full path
        /// </summary>
        string FullPath { get; }

        /// <summary>
        /// error text if any else null
        /// </summary>
        string Error { get; set; }

        /// <summary>
        /// indicates if file is available or not
        /// </summary>
        bool IsFileAvailable { get; }
    }
}
