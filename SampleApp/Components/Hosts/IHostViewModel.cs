using System.ComponentModel;

using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

using SampleApp.Components.Data.Tree;

using WPFUtilities.ComponentModels;
using WPFUtilities.Components.ServiceComponent;

namespace SampleApp.Components.Hosts
{
    public interface IHostViewModel : ITreeDataGridRowViewModel, IModelBase
    {
        /// <summary>
        /// parent (tree) model
        /// </summary>
        IHostViewModel ParentViewModel { get; }

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
        BindingList<IHostViewModel> Childs { get; }

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
