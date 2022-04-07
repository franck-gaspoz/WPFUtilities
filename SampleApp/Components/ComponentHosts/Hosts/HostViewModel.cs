using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;

using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

using SampleApp.Components.ComponentHosts.Hosts.Data;

using WPFUtilities.Components.ServiceComponent;
using WPFUtilities.Components.UI.TreeDataGridControl;
using WPFUtilities.Extensions.Reflections;
using WPFUtilities.Extensions.Threading;

namespace SampleApp.Components.ComponentHosts.Hosts
{
    /// <summary>
    /// hosts view model
    /// </summary>
    [DebuggerDisplay("HostViewModel: component host = {ComponentHost}")]
    public class HostViewModel :
        TreeDataGridRowViewModel<IHostViewModel>,
        IHostViewModel
    {
        #region host view properties

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

        /// <inheritdoc/>
        public int LoggersCount
        {
            get => LoggerInformations.Count()
                + MessageLoggers.Count()
                + ScopeLoggers.Count();
        }

        int _optionsCount = 0;
        /// <inheritdoc/>
        public int OptionsCount
        {
            get => _optionsCount;
            protected set
            {
                _optionsCount = value;
                NotifyPropertyChanged();
            }
        }

        int _servicesCount = 0;
        /// <inheritdoc/>
        public int ServicesCount
        {
            get => _servicesCount;
            protected set
            {
                _servicesCount = value;
                NotifyPropertyChanged();
            }
        }

        int _realizedServicesCount = 0;
        /// <inheritdoc/>
        public int RealizedServicesCount { 
            get => _realizedServicesCount;
            set
            {
                _realizedServicesCount = value;
                NotifyPropertyChanged();
            }
        }

        /// <inheritdoc/>
        public BindingList<LoggerModel> LoggerInformations { get; } = new BindingList<LoggerModel>();

        /// <inheritdoc/>
        public BindingList<MessageLoggerModel> MessageLoggers { get; } = new BindingList<MessageLoggerModel>();

        /// <inheritdoc/>
        public BindingList<ScopeLoggerModel> ScopeLoggers { get; } = new BindingList<ScopeLoggerModel>();

        /// <inheritdoc/>
        public BindingList<ServiceModel> RealizedServices { get; } = new BindingList<ServiceModel>();

        /// <inheritdoc/>
        public BindingList<ServiceModel> RegisteredServices { get; } = new BindingList<ServiceModel>();

        #endregion

        /// <inheritdoc/>
        public IHostViewModel Initialize(
            IComponentHost host,
            int level,
            IHostViewModel parentViewModel)
        {
            Name = host.Name;
            ComponentHost = host;
            Level = level;
            this.Parent = parentViewModel;

            host.OnNotNull<IHost>(
                "Host",
                (_host) =>
                {
                    if (_host.Services.GetMember<object>("_realizedServices", out var rservices))
                    {
                        var ms = rservices.GetType().GetMembers(TypeExtensions.DefaultScopeBindingFlags);
                        var keys = rservices.InvokeMethod<IReadOnlyCollection<Type>>("get_Keys");
                        ServicesCount = keys.Count;
                        foreach (var key in keys)
                            RealizedServices.Add(
                                new ServiceModel
                                {
                                    RegisteredType = key,
                                    ResolvedType = null,
                                    Assembly = key.Assembly
                                });
                    }

                    if (_host.Services.GetMember<object>("CallSiteFactory", out var csfactory)
                        && csfactory.GetMember<ICollection>("_callSiteCache", out var cscache))
                    {
                        RealizedServicesCount = cscache.Count;
                        foreach (var key in cscache)
                        {
                            var type = key.GetMember<object>("Key")?.GetMember<Type>("Type");
                            RegisteredServices.Add(
                                new ServiceModel
                                {
                                    RegisteredType = type,
                                    ResolvedType = null,
                                    Assembly = type.Assembly
                                });
                        }
                    }

                    if (_host.GetField<HostOptions>("_options", out var options))
                    {
                        HostOptions = options;
                        OptionsCount = 2;
                    }

                    if (_host.GetField<ILogger>("_logger", out var logger))
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
                            {
                                Add(loggerInformationArray, AddLoggerInformation);
                                NotifyPropertyChanged(nameof(LoggersCount));
                            }
                            if (_logger.GetMember<object>("MessageLoggers", out var messageLoggerArray))
                            {
                                Add(messageLoggerArray, AddMessageLogger);
                                NotifyPropertyChanged(nameof(LoggersCount));
                            }
                            if (_logger.GetMember<object>("ScopeLoggers", out var scopeLoggerInformationArray))
                            {
                                Add(scopeLoggerInformationArray, AddScopeLogger);
                                NotifyPropertyChanged(nameof(LoggersCount));
                            }
                        }
                        HostLoggerDescription =
                            string.Join(Environment.NewLine, GetHostLoggerDescription());
                    }
                });

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
                ("logger informations:",LoggerInformations.Cast<object>().ToList()),
                ("message loggers:",MessageLoggers.Cast<object>().ToList()),
                ("scope loggers:",ScopeLoggers.Cast<object>().ToList())
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
            var data = new LoggerModel
            {
                Category = logger.GetMember<string>("Category"),
                ExternalScope = logger.GetMember<bool>("ExternalScope"),
                Logger = logger.GetMember<ILogger>("Logger"),
                LoggerDescription = logger.GetMember<string>("LoggerDescription"),
                ProviderType = logger.GetMember<Type>("ProviderType"),
                Assembly = logger.GetMember<ILogger>("Logger")?.GetType().Assembly
            };
            LoggerInformations.Add(data);
        }

        void AddMessageLogger(object logger)
        {
            var data = new MessageLoggerModel
            {
                Category = logger.GetMember<string>("Category"),
                Logger = logger.GetMember<ILogger>("Logger"),
                LoggerDescription = logger.GetMember<string>("LoggerDescription"),
                MinLevel = logger.GetMember<LogLevel?>("MinLevel"),
                Assembly = logger.GetMember<ILogger>("Logger")?.GetType().Assembly
            };
            MessageLoggers.Add(data);
        }

        void AddScopeLogger(object logger)
        {
            var data = new ScopeLoggerModel
            {
                ExternalScopeProvider = logger.GetMember<IExternalScopeProvider>("ExternalScopeProvider"),
                Logger = logger.GetMember<ILogger>("Logger"),
                LoggerDescription = logger.GetMember<string>("LoggerDescription"),
                Assembly = logger.GetMember<ILogger>("Logger")?.GetType().Assembly
            };
            ScopeLoggers.Add(data);
        }

    }
}
