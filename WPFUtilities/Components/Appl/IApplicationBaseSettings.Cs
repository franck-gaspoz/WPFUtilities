using System;
using System.Windows;

using Microsoft.Extensions.DependencyInjection;

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
        /// action to be launch at initialization, before main window is created
        /// </summary>
        Action Initialize { get; set; }

        /// <summary>
        /// action to be launch at initialization, after main window is created, before it is shown
        /// </summary>
        Action<Window> InitializeWindow { get; set; }

        /// <summary>
        /// host configure services callback
        /// </summary>
        Action<IServiceCollection> ConfigureServices { get; set; }

        /// <summary>
        /// shutdown time out
        /// </summary>
        int ShutdownTimeout { get; set; }
    }
}
