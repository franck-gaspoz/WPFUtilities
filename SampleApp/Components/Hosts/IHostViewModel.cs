using System.ComponentModel;

using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

using WPFUtilities.ComponentModels;
using WPFUtilities.Components.ServiceComponent;

namespace SampleApp.Components.Hosts
{
    public interface IHostViewModel : IModelBase
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

        #endregion

        /// <summary>
        /// name
        /// </summary>
        string Name { get; set; }

        /// <summary>
        /// host logger
        /// </summary>
        ILogger HostLogger { get; set; }

        /// <summary>
        /// component host
        /// </summary>
        IComponentHost ComponentHost { get; set; }

        /// <summary>
        /// host options
        /// </summary>
        HostOptions HostOptions { get; set; }

        /// <summary>
        /// hosts
        /// </summary>
        BindingList<IHostViewModel> Hosts { get; }

        /// <summary>
        /// initialize from a component host
        /// </summary>
        /// <param name="host">host</param>
        /// <param name="level">level</param>
        /// <returns></returns>
        IHostViewModel Initialize(IComponentHost host, int level);
    }
}
