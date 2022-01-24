
using Microsoft.Extensions.Logging.Configuration;
using Microsoft.Extensions.Options;

namespace WPFUtilities.Components.Logging.ListLogger
{
    /// <summary>
    /// list logger configuration setup
    /// </summary>
    public class ListLoggerConfigurationSetup
        : ConfigureFromConfigurationOptions<ListLoggerConfiguration>
    {
        /// <summary>
        /// build a new instance
        /// </summary>
        /// <param name="providerConfiguration"></param>
        public ListLoggerConfigurationSetup(ILoggerProviderConfiguration<ListLoggerProvider> providerConfiguration)
            : base(providerConfiguration.Configuration)
        {

        }
    }
}
