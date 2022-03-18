using System;
using System.Collections.Generic;
using System.ComponentModel;

using WPFUtilities.ComponentModels;

namespace SampleApp.Components.Data.Tree
{
    /// <summary>
    /// a 'tree style' data grid row view model
    /// </summary>
    public class TreeDataGridRowViewModel<ViewModelBase>
        : ModelBase,
        ITreeDataGridRowViewModel<ViewModelBase>
        where ViewModelBase :
            IModelBase,
            ITreeDataGridRowViewModel,
            ITreeDataGridRowViewModel<ViewModelBase>
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

        bool _isExpanded = true;
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
        public int ChildsCount => Childs?.Count ?? 0;

        /// <inheritdoc/>
        public bool IsFolded
        {
            get
            {
                ITreeDataGridRowViewModel<ViewModelBase> parent = Parent;
                while (parent != null)
                {
                    if (!parent.IsExpanded) return true;
                    parent = parent.Parent;
                }
                return false;
            }
        }

        ViewModelBase _parent = default;
        /// <inheritdoc/>
        public ViewModelBase Parent
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
            if (e.PropertyName == nameof(IsExpanded))
            {
                NotifyPropertyChanged(nameof(IsFolded));
                if (ChildsCount > 0)
                    NotifyPropertyChanged(nameof(IsExpanded));
            }
        }

        /// <inheritdoc/>
        public BindingList<ViewModelBase> Childs { get; }
            = new BindingList<ViewModelBase>();

        /// <inheritdoc/>
        public ITreeDataGridRowViewModel GetParent() => Parent;

        /// <inheritdoc/>
        public IEnumerable<ITreeDataGridRowViewModel> GetChilds()
            => throw new NotImplementedException(); // Childs.AsEnumerable();

        #endregion

        /// <summary>
        /// creates a new instance
        /// </summary>
        public TreeDataGridRowViewModel()
        {
            Childs.ListChanged += (o, e) =>
            {
                NotifyPropertyChanged(nameof(ChildsCount));
            };
        }
    }
}
