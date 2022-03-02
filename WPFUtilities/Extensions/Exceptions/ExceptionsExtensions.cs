using System;

namespace WPFUtilities.Extensions.Exceptions
{
    /// <summary>
    /// exceptions extensions
    /// </summary>
    public static class ExceptionsExtensions
    {
        /// <summary>
        /// builds a message from the exception messages and its inners exceptions messages
        /// </summary>
        /// <param name="exception"></param>
        public static string GetFullMessage(this Exception exception)
            => exception.Message
                + ((exception.InnerException != null)
                    ? "," + exception.InnerException.GetFullMessage()
                    : string.Empty);
    }
}
