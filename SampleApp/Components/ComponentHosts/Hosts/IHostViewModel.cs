
using System.ComponentModel;

using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

using SampleApp.Components.ComponentHosts.Hosts.Data;
using SampleApp.Components.Data.Tree;

using WPFUtilities.ComponentModels;
using WPFUtilities.Components.ServiceComponent;

namespace SampleApp.Components.ComponentHosts.Hosts
{
    public interface IHostViewModel :
        ITreeDataGridRowViewModel,
        ITreeDataGridRowViewModel<IHostViewModel>,
        IModelBase
    {
        /// <summary>
        /// name
        /// </summary>
        string Name { get; set; }

        /// <summary>
        /// host logger
        /// </summary>
        ILogger HostLogger { get; set; }

        /// <summary>
        /// host logger description
        /// </summary>
        string HostLoggerDescription { get; set; }

        /// <summary>
        /// loggers count
        /// </summary>
        int LoggersCount { get; }

        /// <summary>
        /// component host
        /// </summary>
        IComponentHost ComponentHost { get; set; }

        /// <summary>
        /// host options
        /// </summary>
        HostOptions HostOptions { get; set; }

        /// <summary>
        /// options count
        /// </summary>
        int OptionsCount { get; }

        /// <summary>
        /// services count
        /// </summary>
        int ServicesCount { get; }

        /// <summary>
        /// loggers informations
        /// </summary>
        BindingList<LoggerModel> LoggerInformations { get; }

        /// <summary>
        /// message loggers
        /// </summary>
        BindingList<MessageLoggerModel> MessageLoggers { get; }

        /// <summary>
        /// scope loggers
        /// </summary>
        BindingList<ScopeLoggerModel> ScopeLoggers { get; }

        /// <summary>
        /// initialize from a component host
        /// </summary>
        /// <param name="host">host</param>
        /// <param name="level">level</param>
        /// <param name="parentViewModel">parentV view model</param>
        /// <returns></returns>
        IHostViewModel Initialize(
            IComponentHost host,
            int level,
            IHostViewModel parentViewModel);
    }
}
