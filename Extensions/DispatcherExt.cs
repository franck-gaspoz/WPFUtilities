using System;
using System.Windows.Threading;

namespace WPFUtilities.Extensions
{
    public static class DispatcherExt
    {
        public static DispatcherOperation InvokeWhenUIReady(
            this Dispatcher d,
            Action a
            ) => d.BeginInvoke(a, DispatcherPriority.Background);

        public static DispatcherOperation BeginInvoke(
            this Dispatcher d,
            DispatcherPriority dispatcherPriority,
            Action a
            ) => d.BeginInvoke(a, dispatcherPriority);
    }
}
