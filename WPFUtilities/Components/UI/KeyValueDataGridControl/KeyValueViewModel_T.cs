using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

using WPFUtilities.ComponentModels;

namespace WPFUtilities.Components.UI.KeyValueDataGridControl
{
    public class KeyValueViewModel<ViewModelBase> :
        ModelBase,
        IKeyValueViewModel<ViewModelBase>
        where ViewModelBase :
        IKeyValueItem,
        IModelBase
    {
        /// <inheritdoc/>
        public ObservableCollection<ViewModelBase> Items { get; }
            = new ObservableCollection<ViewModelBase>();

        ViewModelBase _selectedItem = default;
        /// <inheritdoc/>
        public ViewModelBase SelectedItem
        {
            get
            {
                return _selectedItem;
            }
            set
            {
                _selectedItem = value;
                NotifyPropertyChanged();
            }
        }

        /// <inheritdoc/>
        public IKeyValueItem GetSelectedItem()
            => SelectedItem;

        /// <inheritdoc/>
        public IEnumerable<IKeyValueItem> GetItems()
            => Items.Cast<IKeyValueItem>()
                    .AsEnumerable();
    }
}
