
using SampleApp.Components.Data.KeyValue;

using WPFUtilities.ComponentModels;

namespace SampleApp.Components.ComponentHosts.Loggers
{
    /// <summary>
    /// 
    /// </summary>
    public class LoggerViewModel :
        KeyValueItem,
        IKeyValueItem,
        IModelBase
    {
        /// <summary>
        /// group name
        /// </summary>
        public string GroupName { get; set; }
    }
}
