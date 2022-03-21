using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

using WPFUtilities.ComponentModels;

namespace SampleApp.Components.Data.Tree
{
    /// <inheritdoc/>
    public class TreeDataGridViewModel<ViewModelBase>
        : ModelBase,
        ITreeDataGridViewModel<ViewModelBase>

        where ViewModelBase :
            ITreeDataGridRowViewModel<ViewModelBase>,
            IModelBase
    {
        /// <summary>
        /// items
        /// </summary>
        public BindingList<ViewModelBase> Items { get; }
            = new BindingList<ViewModelBase>();

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
        public ITreeDataGridRowViewModel GetSelectedItem()
            => SelectedItem;

        /// <inheritdoc/>
        public IEnumerable<ITreeDataGridRowViewModel> GetItems()
            => Items.Cast<ITreeDataGridRowViewModel>()
                    .AsEnumerable();
    }
}
