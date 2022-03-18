using System.Collections.ObjectModel;

using WPFUtilities.ComponentModels;

namespace SampleApp.Components.Data.KeyValue
{
    public class KeyValueViewModel : ModelBase, IKeyValueViewModel
    {
        /// <inheritdoc/>
        public ObservableCollection<IKeyValueItem> Items { get; } = new ObservableCollection<IKeyValueItem>();

        IKeyValueItem _selectedItem = null;
        /// <inheritdoc/>
        public IKeyValueItem SelectedItem
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
    }
}
