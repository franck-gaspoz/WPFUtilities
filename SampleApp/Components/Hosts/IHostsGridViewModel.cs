using System.ComponentModel;

using WPFUtilities.ComponentModels;

namespace SampleApp.Components.Hosts
{
    /// <summary>
    /// hosts grid view model
    /// </summary>
    public interface IHostsGridViewModel : IModelBase
    {
        /// <summary>
        /// items
        /// </summary>
        BindingList<IHostViewModel> Items { get; }
    }
}
