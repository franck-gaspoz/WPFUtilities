using System.Reflection;

using SampleApp.Components.Data.KeyValue;

namespace SampleApp.Components.ComponentHosts.Services
{
    /// <summary>
    /// service view model
    /// </summary>
    public interface IServiceViewModel : IKeyValueItem
    {
        /// <summary>
        /// service group name
        /// </summary>
        string GroupName { get; set; }

        /// <summary>
        /// logger assembly
        /// </summary>
        Assembly Assembly { get; set; }
    }
}