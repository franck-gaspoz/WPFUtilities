using Microsoft.Extensions.Logging;

namespace WPFUtilities.Components.Application
{
    /// <summary>
    /// application logging settings
    /// </summary>
    public class ApplicationLoggingSettings : IApplicationLoggingSettings
    {
        /// <inheritdoc/>
        public LogLevel MinimumLogLevel { get; set; } = LogLevel.Trace;

        /// <inheritdoc/>
        public bool LogConsole { get; set; } = true;
    }
}
