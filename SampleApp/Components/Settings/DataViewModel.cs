using System.Collections.ObjectModel;

using WPFUtilities.ComponentModels;

namespace SampleApp.Components.Settings
{
    public class DataViewModel : ModelBase, IDataViewModel
    {
        /// <inheritdoc/>
        public ObservableCollection<DataItem> Items { get; } = new ObservableCollection<DataItem>();
    }
}
