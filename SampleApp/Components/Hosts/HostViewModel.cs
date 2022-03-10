using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

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
        ITreeDataGridRowViewModel<IHostViewModel>
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

        /// <inheritdoc/>
        public ITreeDataGridRowViewModel GetParent() => Parent;

        /// <inheritdoc/>
        public IEnumerable<ITreeDataGridRowViewModel> GetChilds() => Childs.AsEnumerable();

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

        string _hostLoggerDescription = null;
        /// <inheritdoc/>
        public string HostLoggerDescription
        {
            get
            {
                return _hostLoggerDescription;
            }
            set
            {
                _hostLoggerDescription = value;
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
        /// loggers informations
        /// </summary>
        public List<object> LoggerInformations = new List<object>();

        /// <summary>
        /// message loggers
        /// </summary>
        public List<object> MessageLoggers = new List<object>();

        /// <summary>
        /// scope loggers
        /// </summary>
        public List<object> ScopeLoggers = new List<object>();

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
            {
                HostLogger = logger;
                if (HostLogger.GetMember<object>("_logger", out var _logger))
                {
                    void Add(object array, Action<object> handleObject)
                    {
                        var enumerator = array.InvokeMethod<IEnumerator>("GetEnumerator");
                        while (enumerator.MoveNext())
                            handleObject(enumerator.Current);
                    }

                    if (_logger.GetMember<object>("Loggers", out var loggerInformationArray))
                        Add(loggerInformationArray, AddLoggerInformation);
                    if (_logger.GetMember<object>("MessageLoggers", out var messageLoggerArray))
                        Add(messageLoggerArray, AddMessageLogger);
                    if (_logger.GetMember<object>("ScopeLoggers", out var scopeLoggerInformationArray))
                        Add(scopeLoggerInformationArray, AddScopeLogger);
                }
            }
            HostLoggerDescription =
                string.Join(Environment.NewLine, GetHostLoggerDescription());
            return this;
        }

        string GetHostLoggerDescription()
        {
            var sb = new StringBuilder();
            string GetLoggerDescription(object obj)
                => obj.GetMember<string>("LoggerDescription")
                    ?.Split('.')
                    ?.Last()
                    ?.Replace("Logger", "");

            var t = new (string key, List<object> values)[]
            {
                ("logger informations:",LoggerInformations),
                ("message loggers:",MessageLoggers),
                ("scope loggers:",ScopeLoggers)
            };
            foreach (var t2 in t)
            {
                sb.Append(t2.key);
                var t3 = new List<string>();
                foreach (var o in t2.values)
                    t3.Add(GetLoggerDescription(o));
                sb.AppendLine(string.Join(",", t3));
            }
            return sb.ToString().Trim();
        }

        void AddLoggerInformation(object logger)
        {
            var data = new
            {
                Category = logger.GetMember<string>("Category"),
                ExternalScope = logger.GetMember<bool>("ExternalScope"),
                Logger = logger.GetMember<ILogger>("Logger"),
                LoggerDescription = logger.GetMember<ILogger>("Logger")?.ToString(),
                ProviderType = logger.GetMember<Type>("ProviderType")
            };
            LoggerInformations.Add(data);
        }

        void AddMessageLogger(object logger)
        {
            var data = new
            {
                Category = logger.GetMember<string>("Category"),
                Logger = logger.GetMember<ILogger>("Logger"),
                LoggerDescription = logger.GetMember<ILogger>("Logger")?.ToString(),
                MinLevel = logger.GetMember<LogLevel?>("MinLevel"),
            };
            MessageLoggers.Add(data);
        }

        void AddScopeLogger(object logger)
        {
            var data = new
            {
                ExternalScopeProvider = logger.GetMember<IExternalScopeProvider>("ExternalScopeProvider"),
                Logger = logger.GetMember<ILogger>("Logger"),
                LoggerDescription = logger.GetMember<ILogger>("Logger")?.ToString()
            };
            ScopeLoggers.Add(data);
        }

    }
}
