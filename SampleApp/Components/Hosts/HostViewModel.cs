using System.ComponentModel;

using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

using WPFUtilities.ComponentModels;
using WPFUtilities.Components.ServiceComponent;
using WPFUtilities.Extensions.Reflections;

namespace SampleApp.Components.Hosts
{
    /// <summary>
    /// hosts view model
    /// </summary>
    public class HostViewModel : ModelBase, IHostViewModel
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
        public int ChildsCount => Hosts?.Count ?? 0;

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

        /// <summary>
        /// hosts
        /// </summary>
        public BindingList<IHostViewModel> Hosts { get; }
            = new BindingList<IHostViewModel>();

        /// <inheritdoc/>
        public IHostViewModel Initialize(
            IComponentHost host,
            int level)
        {
            Name = host.Name;
            ComponentHost = host;
            Level = level;

            if (host.Host.GetField<HostOptions>("_options", out var options))
                HostOptions = options;
            if (host.Host.GetField<ILogger>("_logger", out var logger))
                HostLogger = logger;
            return this;
        }

    }
}
