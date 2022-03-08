using System.ComponentModel;

using WPFUtilities.ComponentModels;

namespace SampleApp.Components.Data.Tree
{
    /// <summary>
    /// view model of a 'tree style' data grid row
    /// </summary>
    public interface ITreeDataGridRowViewModel<ViewModelBase> :
        ITreeDataGridRowViewModel,
        IModelBase
        where ViewModelBase : IModelBase
    {
        /// <summary>
        /// parent (tree) model
        /// </summary>
        ViewModelBase Parent { get; }

        /// <summary>
        /// hosts
        /// </summary>
        BindingList<ViewModelBase> Childs { get; }
    }
}
