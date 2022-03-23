
using SampleApp.Components.Data.KeyValue;

using WPFUtilities.ComponentModels;

namespace SampleApp.Components.ComponentHosts.Services
{
    /// <summary>
    /// 
    /// </summary>
    public class ServiceViewModel :
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
