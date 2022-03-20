﻿using System.ComponentModel;

using WPFUtilities.ComponentModels;

namespace SampleApp.Components.Data.Tree
{
    /// <summary>
    /// view model of a 'tree style' data grid
    /// </summary>
    public interface ITreeDataGridViewModel<ViewModelBase> :
        ITreeDataGridViewModel,
        IModelBase

        where ViewModelBase :
            ITreeDataGridRowViewModel<ViewModelBase>,
            IModelBase
    {
        /// <summary>
        /// items
        /// </summary>
        BindingList<ViewModelBase> Items { get; }

        /// <summary>
        /// selected item
        /// </summary>
        ViewModelBase SelectedItem { get; set; }
    }
}
