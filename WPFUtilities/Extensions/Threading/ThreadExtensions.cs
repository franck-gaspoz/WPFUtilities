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
        public static void OnNotNull<T>(
            this object source,
            string memberName,
            Action<T> action,
            int maxRetryCount = DefaultMaxRetryCount)
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
                action((T)value);
            }
        }
    }
}
