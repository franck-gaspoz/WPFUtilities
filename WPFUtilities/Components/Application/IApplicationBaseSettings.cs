using System;

namespace WPFUtilities.Components.Application
{
    /// <summary>
    /// application base settings
    /// </summary>
    public interface IApplicationBaseSettings
    {
        /// <summary>
        /// main window component service interface type
        /// </summary>
        Type MainWindowComponentInterfaceType { get; set; }

        /// <summary>
        /// main window component service implementation type
        /// </summary>
        Type MainWindowComponentImplementationType { get; set; }

        /// <summary>
        /// main window type
        /// </summary>
        Type MainWindowType { get; set; }

        /// <summary>
        /// indicates if must show main window after created
        /// </summary>
        bool ShowWindow { get; set; }

        /// <summary>
        /// indicates if main window is shown as a dialog if it is shown after created
        /// </summary>
        bool IsMainWindowDialog { get; set; }

        /// <summary>
        /// default culture for threads and thread ui
        /// </summary>
        string DefaultCulture { get; set; }

        /// <summary>
        /// shutdown time out
        /// </summary>
        int ShutdownTimeout { get; set; }

        /// <summary>
        /// application logging settings
        /// </summary>
        IApplicationLoggingSettings ApplicationLoggingSettings { get; set; }
    }
}
