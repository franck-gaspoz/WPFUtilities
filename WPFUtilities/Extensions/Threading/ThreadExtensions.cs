using System;
using System.Threading;
using System.Threading.Tasks;

using WPFUtilities.Extensions.Reflections;

namespace WPFUtilities.Extensions.Threading
{
    /// <summary>
    /// threads extensions
    /// </summary>
    public static class ThreadExtensions
    {
        /// <summary>
        /// default max retry count
        /// </summary>
        public const int DefaultMaxRetryCount = 3;

        /// <summary>
        /// default wait time before trigger action
        /// </summary>
        public const int DefaultWaitTimeBeforeTriggerAction = 10000;

        /// <summary>
        /// default sleep delay ms
        /// </summary>
        public const int DefaultSleepDelay = 200;

        /// <summary>
        /// perform an action once a member of an object is not null. if not null performs action immediately,
        /// else waits a const time and retry a max times using threads
        /// </summary>
        /// <typeparam name="T">exepected value type</typeparam>
        /// <param name="source">source object</param>
        /// <param name="memberName">source member</param>
        /// <param name="action">action to trigger</param>
        /// <param name="maxRetryCount">max retry count (default DefaultMaxRetryCount)</param>
        /// <param name="waitBeforeTriggerAction">if true wait 'waitTimeBeforeTriggerAction' ms before trigger action</param>
        /// <param name="waitTimeBeforeTriggerAction">time to wait before triggera action if wait is enabled</param>
        public static void OnNotNull<T>(
            this object source,
            string memberName,
            Action<T> action,
            int maxRetryCount = DefaultMaxRetryCount,
            bool waitBeforeTriggerAction = false,
            int waitTimeBeforeTriggerAction = DefaultWaitTimeBeforeTriggerAction)
        {
            if (source == null) return;
            var value = source.GetMember<object>(memberName);
            if (value == null)
            {
                if (maxRetryCount == 0) return;
                Task.Run(() =>
                {
                    Thread.Sleep(DefaultSleepDelay);
                    source.OnNotNull(memberName, action, maxRetryCount - 1);
                });
            }
            else
            {
                if (waitBeforeTriggerAction)
                    Task.Run(() =>
                    {
                        Thread.Sleep(DefaultWaitTimeBeforeTriggerAction);
                        action((T)value);
                    });
                else
                    action((T)value);
            }
        }
    }
}
