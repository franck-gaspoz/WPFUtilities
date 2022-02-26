using System;
using System.Windows;

namespace WPFUtilities.Components.Application
{
    /// <summary>
    /// application base settings
    /// </summary>
    public class ApplicationBaseSettings : IApplicationBaseSettings
    {
        /// <inheritdoc/>
        public Type MainWindowComponentInterfaceType { get; set; }

        /// <inheritdoc/>
        public Type MainWindowComponentImplementationType { get; set; }

        /// <inheritdoc/>
        public Type MainWindowType { get; set; }

        /// <inheritdoc/>
        public bool ShowWindow { get; set; } = true;

        /// <inheritdoc/>
        public bool IsMainWindowDialog { get; set; } = true;

        /// <inheritdoc/>
        public string DefaultCulture { get; set; } = null;

        /// <inheritdoc/>
        public Action<Window> InitializeMainWindow { get; set; }

        /// <inheritdoc/>
        public int ShutdownTimeout { get; set; } = 5;

        /// <inheritdoc/>
        public IApplicationLoggingSettings ApplicationLoggingSettings { get; set; }
            = new ApplicationLoggingSettings();
    }
}
