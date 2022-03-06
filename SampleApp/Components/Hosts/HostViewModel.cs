using System.ComponentModel;

using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

using SampleApp.Components.Data.Tree;

using WPFUtilities.ComponentModels;
using WPFUtilities.Components.ServiceComponent;
using WPFUtilities.Extensions.Reflections;

namespace SampleApp.Components.Hosts
{
    /// <summary>
    /// hosts view model
    /// </summary>
    public class HostViewModel :
        ModelBase,
        IHostViewModel,
        ITreeDataGridVRowViewModel<IHostViewModel>
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
        public int ChildsCount => Childs?.Count ?? 0;

        /// <inheritdoc/>
        public bool IsFolded
        {
            get
            {
                IHostViewModel parent = ParentViewModel;
                while (parent != null)
                {
                    if (!parent.IsExpanded) return true;
                    parent = parent.ParentViewModel;
                }
                return false;
            }
        }

        IHostViewModel _parentViewModel = null;
        /// <inheritdoc/>
        public IHostViewModel ParentViewModel
        {
            get
            {
                return _parentViewModel;
            }
            set
            {
                if (_parentViewModel != null)
                    _parentViewModel.PropertyChanged -= ParentIsExpandedPropertyChanged;
                _parentViewModel = value;
                if (_parentViewModel != null)
                    _parentViewModel.PropertyChanged += ParentIsExpandedPropertyChanged;
                NotifyPropertyChanged();
            }
        }

        private void ParentIsExpandedPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(IHostViewModel.IsExpanded))
            {
                NotifyPropertyChanged(nameof(IsFolded));
                if (ChildsCount > 0)
                    NotifyPropertyChanged(nameof(IsExpanded));
            }
        }

        IHostViewModel _parent = null;
        /// <summary>
        /// parent tree
        /// </summary>
        public IHostViewModel Parent
        {
            get
            {
                return _parent;
            }
            set
            {
                _parent = value;
                NotifyPropertyChanged();
            }
        }

        /// <summary>
        /// childs
        /// </summary>
        public BindingList<IHostViewModel> Childs { get; }
            = new BindingList<IHostViewModel>();

        #endregion

        string _name = null;
        /// <inheritdoc/>
        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                _name = value;
                NotifyPropertyChanged();
            }
        }

        ILogger _hostLogger = null;
        /// <inheritdoc/>
        public ILogger HostLogger
        {
            get
            {
                return _hostLogger;
            }
            set
            {
                _hostLogger = value;
                NotifyPropertyChanged();
            }
        }

        HostOptions _hostOptions = null;
        /// <summary>
        /// summary
        /// </summary>
        public HostOptions HostOptions
        {
            get
            {
                return _hostOptions;
            }
            set
            {
                _hostOptions = value;
                NotifyPropertyChanged();
            }
        }

        /// <inheritdoc/>
        public IComponentHost ComponentHost { get; set; }

        /// <inheritdoc/>
        public IHostViewModel Initialize(
            IComponentHost host,
            int level,
            IHostViewModel parentViewModel)
        {
            Name = host.Name;
            ComponentHost = host;
            Level = level;
            this.ParentViewModel = parentViewModel;

            if (host.Host.GetField<HostOptions>("_options", out var options))
                HostOptions = options;
            if (host.Host.GetField<ILogger>("_logger", out var logger))
                HostLogger = logger;
            return this;
        }

    }
}
