using System.ComponentModel;

using WPFUtilities.ComponentModels;

namespace WPFUtilities.Components.Logging.ListLogger
{
    /// <summary>
    /// list logger model
    /// </summary>
    public class ListLoggerModel : ModelBase, IModelBase, IListLoggerModel
    {
        /// <inheritdoc/>
        public BindingList<string> LogItems { get; }
            = new BindingList<string>();
    }
}
