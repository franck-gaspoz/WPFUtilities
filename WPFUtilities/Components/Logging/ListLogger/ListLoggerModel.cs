using System.Collections.Generic;

namespace WPFUtilities.Components.Logging.ListLogger
{
    /// <summary>
    /// list logger model
    /// </summary>
    public class ListLoggerModel
    {
        /// <summary>
        /// log items
        /// </summary>
        public List<string> LogItems { get; }
            = new List<string>();
    }
}
