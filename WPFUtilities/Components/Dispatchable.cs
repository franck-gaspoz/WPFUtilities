using System;
using System.Windows;
using System.Windows.Threading;

namespace WPFUtilities.Components
{
    /// <summary>
    /// object that has a dispatcher
    /// </summary>
    public class Dispatchable
    {
        /// <summary>
        /// dispatcher
        /// </summary>
        protected Dispatcher Dispatcher;

        /// <summary>
        /// build a new instance. set dispatcher to application dispatcher
        /// </summary>
        public Dispatchable()
        {
            if (Application.Current != null)
                Dispatcher = Application.Current.Dispatcher;
        }

        /// <summary>
        /// build a new instance. assign the given dispatcher
        /// </summary>
        /// <param name="dispatcher">dispatcher</param>
        public Dispatchable(Dispatcher dispatcher)
            => Dispatcher = dispatcher;

        /// <summary>
        /// asynchronously invoke an action
        /// </summary>
        /// <param name="action">action</param>
        /// <param name="priority">dispatch priority</param>
        public void BeginInvoke(
            Action action,
            DispatcherPriority priority = DispatcherPriority.Normal
            )
        {
            this.Dispatcher.BeginInvoke(action, priority);
        }

        /// <summary>
        /// asynchronously invoke an action as soon that ui is ready. doesn't wait before invoking.
        /// </summary>
        /// <param name="action">action</param>
        /// <param name="priority">priority</param>
        public void InvokeWhenUIReady(
            Action action,
            DispatcherPriority priority = DispatcherPriority.Background
            )
        {
            this.Dispatcher.BeginInvoke(action, priority);
        }

        /// <summary>
        /// invoke an action
        /// </summary>
        /// <param name="action">action</param>
        /// <param name="priority">dispatch priority</param>
        public void Invoke(
            Action action,
            DispatcherPriority priority = DispatcherPriority.Normal
            )
        {
            this.Dispatcher.Invoke(action, priority);
        }

        /// <summary>
        /// perform an action in a try/catch block
        /// </summary>
        /// <param name="action"></param>
        /// <param name="onErrorAction"></param>
        protected void TryCatch(
            Action action,
            Action<Exception, string> onErrorAction = null)
        {
            try
            {
                action();
            }
            catch (Exception exception)
            {
                BeginInvoke(() =>
                {
                    var message = "";
                    while (exception != null)
                    {
                        if (!string.IsNullOrWhiteSpace(message))
                            message += "\r\n";
                        message += exception.Message;
                        exception = exception.InnerException;
                    }
                    onErrorAction?.Invoke(exception, message);
                });
            }
        }
    }
}