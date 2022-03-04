using System.Collections.ObjectModel;

using WPFUtilities.ComponentModels;

namespace SampleApp.Components.Data.KeyValue
{
    public class KeyValueViewModel : ModelBase, IKeyValueViewModel
    {
        /// <inheritdoc/>
        public ObservableCollection<KeyValueItem> Items { get; } = new ObservableCollection<KeyValueItem>();
    }
}
