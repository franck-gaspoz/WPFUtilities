
using System.Reflection;

using SampleApp.Components.Data.KeyValue;

namespace SampleApp.Components.ComponentHosts.Services
{
    /// <summary>
    /// service view model
    /// </summary>
    public class ServiceViewModel :
        KeyValueItem,
        IServiceViewModel
    {
        /// <inheritdoc/>
        public string GroupName { get; set; }

        /// <inheritdoc/>
        public Assembly Assembly { get; set; }
    }
}
