using System.ComponentModel;

using WPFUtilities.ComponentModels;

namespace SampleApp.Components.Data.Tree
{
    /// <summary>
    /// view model of a 'tree style' data grid row
    /// </summary>
    public interface ITreeDataGridVRowViewModel<ViewModelBase> : IModelBase
        where ViewModelBase : IModelBase
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
        ViewModelBase Parent { get; }

        /// <summary>
        /// hosts
        /// </summary>
        BindingList<ViewModelBase> Childs { get; }

        #endregion
    }
}
