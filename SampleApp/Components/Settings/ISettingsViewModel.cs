using System.ComponentModel;

using WPFUtilities.ComponentModels;

namespace SampleApp.Components.Settings
{
    public interface ISettingsViewModel : IModelBase
    {
        /// <summary>
        /// environment name
        /// </summary>
        string EnvironmentName { get; set; }

        /// <summary>
        /// application name
        /// </summary>
        string ApplicationName { get; set; }

        /// <summary>
        /// content root path
        /// </summary>
        string ContentRootPath { get; set; }

        /// <summary>
        /// selected provider
        /// </summary>
        object SelectedProvider { get; set; }

        /// <summary>
        /// providers
        /// </summary>
        BindingList<object> Providers { get; }
    }
}
