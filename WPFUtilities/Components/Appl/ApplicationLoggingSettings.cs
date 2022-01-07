using Microsoft.Extensions.Logging;

namespace WPFUtilities.Components.Appl
{
    /// <summary>
    /// application logging settings
    /// </summary>
    public class ApplicationLoggingSettings : IApplicationLoggingSettings
    {
        /// <inheritdoc/>
        public LogLevel MinimumLogLevel { get; set; }
    }
}
