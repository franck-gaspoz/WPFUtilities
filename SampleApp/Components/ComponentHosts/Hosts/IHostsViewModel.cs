
using System.ComponentModel;

using WPFUtilities.ComponentModels;

namespace SampleApp.Components.ComponentHosts.Hosts
{
    /// <summary>
    /// interface hosts view model
    /// </summary>
    public interface IHostsViewModel : IModelBase
    {
        /// <summary>
        /// hosts
        /// </summary>
        BindingList<IHostViewModel> Hosts { get; }
    }
}
