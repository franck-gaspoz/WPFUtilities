using System;
using System.Windows.Threading;

using syswin = System.Windows;

namespace WPFUtilities.Extensions.Window
{
    /// <summary>
    /// window extensions
    /// </summary>
    public static class WindowExtensions
    {
        /// <summary>
        /// wait until ui is ready (any display operation finished)
        /// </summary>
        /// <param name="window">window</param>
        public static void WaitForUI(this syswin.Window window)
            => window.Dispatcher.BeginInvoke(
                new Action(() => { }),
                DispatcherPriority.ContextIdle,
                null);
    }
}
