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

        /// <summary>
        /// selected item
        /// </summary>
        public ViewModelBase SelectedItem { get; set; }

        /// <inheritdoc/>
        public ITreeDataGridRowViewModel GetSelectedItem()
            => SelectedItem;

        /// <inheritdoc/>
        public IEnumerable<ITreeDataGridRowViewModel> GetItems()
            => Items.Cast<ITreeDataGridRowViewModel>()
                    .AsEnumerable();
    }
}
