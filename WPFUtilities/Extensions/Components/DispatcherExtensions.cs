using System;
using System.Windows.Threading;

namespace WPFUtilities.Extensions.Components
{
    /// <summary>
    /// dispatcher extensions
    /// </summary>
    public static class DispatcherExtensions
    {
        /// <summary>
        /// invoke asynchronously an action as soon ui is ready
        /// </summary>
        /// <param name="dispatcher">dispatcher</param>
        /// <param name="action">action</param>
        /// <returns>dispatcher operation</returns>
        public static DispatcherOperation InvokeWhenUIReady(
            this Dispatcher dispatcher,
            Action action
            ) => dispatcher.BeginInvoke(
                action,
                DispatcherPriority.Background);

        /// <summary>
        /// invoke asynchronously an action
        /// </summary>
        /// <param name="dispatcher">dispatcher</param>
        /// <param name="dispatcherPriority">priority</param>
        /// <param name="action">action</param>
        /// <returns>dispatcher operation</returns>
        public static DispatcherOperation BeginInvoke(
            this Dispatcher dispatcher,
            DispatcherPriority dispatcherPriority,
            Action action
            ) => dispatcher.BeginInvoke(action, dispatcherPriority);
    }
}
