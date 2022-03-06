using System.ComponentModel;

using WPFUtilities.ComponentModels;

namespace SampleApp.Components.Data.Tree
{
    /// <summary>
    /// a 'tree style' data grid row view model
    /// </summary>
    public abstract class TreeDataGridRowViewModel : ModelBase, ITreeDataGridRowViewModel
    {
        #region tree properties

        int _level = 0;
        /// <inheritdoc/>
        public int Level
        {
            get
            {
                return _level;
            }
            set
            {
                _level = value;
                NotifyPropertyChanged();
            }
        }

        bool _isExpanded = false;
        /// <inheritdoc/>
        public bool IsExpanded
        {
            get
            {
                return _isExpanded;
            }
            set
            {
                _isExpanded = value;
                NotifyPropertyChanged();
            }
        }

        /// <inheritdoc/>
        public abstract int ChildsCount { get; }

        /// <inheritdoc/>
        public bool IsFolded
        {
            get
            {
                ITreeDataGridRowViewModel parent = Parent;
                while (parent != null)
                {
                    if (!parent.IsExpanded) return true;
                    parent = parent.Parent;
                }
                return false;
            }
        }

        ITreeDataGridRowViewModel _parent = null;
        /// <inheritdoc/>
        public ITreeDataGridRowViewModel Parent
        {
            get
            {
                return _parent;
            }
            set
            {
                if (_parent != null)
                    _parent.PropertyChanged -= ParentIsExpandedPropertyChanged;
                _parent = value;
                if (_parent != null)
                    _parent.PropertyChanged += ParentIsExpandedPropertyChanged;
                NotifyPropertyChanged();
            }
        }

        private void ParentIsExpandedPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(ITreeDataGridRowViewModel.IsExpanded))
            {
                NotifyPropertyChanged(nameof(IsFolded));
                if (ChildsCount > 0)
                    NotifyPropertyChanged(nameof(IsExpanded));
            }
        }

        /// <inheritdoc/>
        public BindingList<ITreeDataGridRowViewModel> Childs { get; }

        #endregion
    }
}
