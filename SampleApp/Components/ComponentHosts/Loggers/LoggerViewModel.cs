
using SampleApp.Components.Data.KeyValue;

namespace SampleApp.Components.ComponentHosts.Loggers
{
    /// <summary>
    /// logger view model
    /// </summary>
    public class LoggerViewModel :
        KeyValueItem,
        ILoggerViewModel
    {
        /// <inheritdoc/>
        public string GroupName { get; set; }
    }
}
