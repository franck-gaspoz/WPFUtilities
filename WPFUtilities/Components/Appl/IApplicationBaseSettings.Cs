using System;
using System.Windows;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace WPFUtilities.Components.Appl
{
    /// <summary>
    /// application base settings
    /// </summary>
    public interface IApplicationBaseSettings
    {
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
        /// action to be launch at initialization, before main window is created [initialization step 1]
        /// </summary>
        Action Initialize { get; set; }

        /// <summary>
        /// action to be launch at initialization, after main window is created, before it is shown [initialization step 5]
        /// </summary>
        Action<Window> InitializeMainWindow { get; set; }

        /// <summary>
        /// host configure logging callback [initialization step 3]
        /// </summary>
        Action<ILoggingBuilder> ConfigureLogging { get; set; }

        /// <summary>
        /// host configure services callback [initialization step 4]
        /// </summary>
        Action<IServiceCollection> ConfigureServices { get; set; }

        /// <summary>
        /// configure host callback [initialization step 2]
        /// </summary>
        Action<IHostBuilder> InitializeHost { get; set; }

        /// <summary>
        /// action callback at application shutdown
        /// </summary>
        Action ShutdownAction { get; set; }

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
