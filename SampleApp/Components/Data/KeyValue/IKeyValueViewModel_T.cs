
using System.Collections.ObjectModel;

using WPFUtilities.ComponentModels;

namespace SampleApp.Components.Data.KeyValue
{
    public interface IKeyValueViewModel<ViewModelBase> :
        IModelBase,
        IKeyValueViewModelBase
        where ViewModelBase :
        IKeyValueItem
    {
        /// <summary>
        /// items list
        /// </summary>
        ObservableCollection<ViewModelBase> Items { get; }

        /// <summary>
        /// selected item
        /// </summary>
        ViewModelBase SelectedItem { get; set; }
    }
}
