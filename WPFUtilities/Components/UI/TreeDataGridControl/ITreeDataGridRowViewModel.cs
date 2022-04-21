
using System.Collections.Generic;

using WPFUtilities.ComponentModels;

namespace WPFUtilities.Components.UI.TreeDataGridControl
{
    /// <summary>
    /// view model of a 'tree style' data grid row
    /// </summary>
    public interface ITreeDataGridRowViewModel : IModelBase
    {
        #region tree properties

        /// <summary>
        /// tree level
        /// </summary>
        int Level { get; set; }

        /// <summary>
        /// is expanded
        /// </summary>
        bool IsExpanded { get; set; }

        /// <summary>
        /// is selected
        /// </summary>
        bool IsSelected { get; set; }

        /// <summary>
        /// childs count
        /// </summary>
        int ChildsCount { get; }

        /// <summary>
        /// indicates if folded by any of its parents
        /// </summary>
        bool IsFolded { get; }

        /// <summary>
        /// parent (tree) model
        /// </summary>
        ITreeDataGridRowViewModel GetParent();

        /// <summary>
        /// childs
        /// </summary>
        IEnumerable<ITreeDataGridRowViewModel> GetChilds();

        #endregion
    }
}
