using System;
using System.Windows;
using System.Windows.Threading;

namespace WPFUtilities.Extensions.Windows
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
        public static void WaitForUI(this Window window)
            => window.Dispatcher.BeginInvoke(
                new Action(() => { }),
                DispatcherPriority.ContextIdle,
                null);
    }
}
