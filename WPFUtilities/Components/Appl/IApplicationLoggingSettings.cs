
using Microsoft.Extensions.Logging;

namespace WPFUtilities.Components.Appl
{
    /// <summary>
    /// application logging settings
    /// </summary>
    public interface IApplicationLoggingSettings
    {
        /// <summary>
        /// minimum log level
        /// </summary>
        LogLevel MinimumLogLevel { get; set; }
    }
}
