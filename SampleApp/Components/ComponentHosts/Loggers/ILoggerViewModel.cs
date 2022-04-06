using System.Reflection;

using WPFUtilities.Components.UI.KeyValueDataGridControl;

namespace SampleApp.Components.ComponentHosts.Loggers
{
    /// <summary>
    /// logger view model
    /// </summary>
    public interface ILoggerViewModel : IKeyValueItem
    {
        /// <summary>
        /// logger group name
        /// </summary>
        string GroupName { get; set; }

        /// <summary>
        /// logger assembly
        /// </summary>
        Assembly Assembly { get; set; }
    }
}