
using System.Collections.Generic;

using WPFUtilities.ComponentModels;

namespace WPFUtilities.Components.UI.KeyValueDataGridControl
{
    public interface IKeyValueViewModelBase : IModelBase
    {
        /// <summary>
        /// selected tree item
        /// </summary>
        IKeyValueItem GetSelectedItem();


        /// <summary>
        /// get tree items
        /// </summary>
        IEnumerable<IKeyValueItem> GetItems();
    }
}
