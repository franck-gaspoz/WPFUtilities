
using Microsoft.Extensions.Logging;

namespace WPFUtilities.Components.Application
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

        /// <summary>
        /// if true enable logging to console
        /// </summary>
        bool LogConsole { get; set; }
    }
}
