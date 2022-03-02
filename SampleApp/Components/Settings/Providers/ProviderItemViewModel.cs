using Microsoft.Extensions.Configuration;

namespace SampleApp.Components.Settings.Providers
{
    /// <summary>
    /// provider item
    /// </summary>
    public class ProviderItemViewModel
    {
        /// <summary>
        /// provider name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// data count
        /// </summary>
        public int? DataCount { get; set; }

        /// <summary>
        /// is file provider
        /// </summary>
        public bool IsProvidingFile { get; set; }

        /// <summary>
        /// configuration provider
        /// </summary>
        public IConfigurationProvider Provider { get; set; }
    }
}
