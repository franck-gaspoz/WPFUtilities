
using System.Collections.ObjectModel;

using WPFUtilities.ComponentModels;

namespace SampleApp.Components.Data
{
    public interface IDataViewModel : IModelBase
    {
        /// <summary>
        /// items list
        /// </summary>
        ObservableCollection<DataItem> Items { get; }
    }
}
