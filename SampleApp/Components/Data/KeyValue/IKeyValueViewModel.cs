
using System.Collections.ObjectModel;

using WPFUtilities.ComponentModels;

namespace SampleApp.Components.Data.KeyValue
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
