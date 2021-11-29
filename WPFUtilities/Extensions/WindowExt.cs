using System;
using System.Windows;
using System.Windows.Threading;

namespace WPFUtilities.Extensions
{
    public static class WindowExt
    {
        public static void WaitUI(this Window window)
            => window.Dispatcher.BeginInvoke(
                new Action(() => { }),
                DispatcherPriority.ContextIdle,
                null);
    }
}
