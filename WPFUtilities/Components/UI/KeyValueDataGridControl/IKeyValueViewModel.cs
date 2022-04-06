
using System.Collections.ObjectModel;

using WPFUtilities.ComponentModels;

namespace WPFUtilities.Components.UI.KeyValueDataGridControl
{
    public interface IKeyValueViewModel : IModelBase
    {
        /// <summary>
        /// items list
        /// </summary>
        ObservableCollection<IKeyValueItem> Items { get; }

        /// <summary>
        /// selected item
        /// </summary>
        IKeyValueItem SelectedItem { get; set; }
    }
}
