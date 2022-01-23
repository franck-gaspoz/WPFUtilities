using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace WPFUtilities.ComponentModels
{
    /// <summary>
    /// host logging configurator
    /// </summary>
    public interface IHostLoggingConfigurator
    {
        /// <summary>
        /// configure logging
        /// </summary>
        /// <param name="context">host builder context</param>
        /// <param name="loggingBuilder">logging builder</param>
        void ConfigureLogging(HostBuilderContext context, ILoggingBuilder loggingBuilder);

    }
}
