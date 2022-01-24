using System.ComponentModel;

namespace WPFUtilities.Components.Logging.ListLogger
{
    /// <summary>
    /// list logger model
    /// </summary>
    public interface IListLoggerModel
    {
        /// <summary>
        /// log items
        /// </summary>
        BindingList<string> LogItems { get; }
    }
}