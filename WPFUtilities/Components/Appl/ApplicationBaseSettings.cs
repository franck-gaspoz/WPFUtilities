using System;
using System.Windows;

using Microsoft.Extensions.Hosting;

namespace WPFUtilities.Components.Appl
{
    /// <summary>
    /// application base settings
    /// </summary>
    public class ApplicationBaseSettings : IApplicationBaseSettings
    {
        /// <summary>
        /// main window component service type
        /// </summary>
        public Type MainWindowComponentType { get; set; }

        /// <inheritdoc/>
        public Type MainWindowType { get; set; }

        /// <summary>
        /// indicates if must show main window after created (default true)
        /// </summary>
        public bool ShowWindow { get; set; } = true;

        /// <summary>
        /// indicates if main window is shown as a dialog if it is shown after created (default true)
        /// </summary>
        public bool IsMainWindowDialog { get; set; } = true;

        /// <summary>
        /// default culture for threads and thread ui, set if not null
        /// </summary>
        public string DefaultCulture { get; set; } = null;

        /// <inheritdoc/>
        public Action<Window> InitializeMainWindow { get; set; }

        /// <inheritdoc/>
        public Action<IHostBuilder> InitializeHost { get; set; }

        /// <inheritdoc/>
        public int ShutdownTimeout { get; set; } = 5;

        /// <inheritdoc/>
        public IApplicationLoggingSettings ApplicationLoggingSettings { get; set; }
            = new ApplicationLoggingSettings();
    }
}
