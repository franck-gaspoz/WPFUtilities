using System.ComponentModel;

using WPFUtilities.ComponentModels;

namespace SampleApp.Components.ComponentHosts
{
    public interface IComponentHostsViewModel : IModelBase
    {
        /// <summary>
        /// selected provider
        /// </summary>
        object SelectedLogger { get; set; }

        /// <summary>
        /// providers
        /// </summary>
        BindingList<object> Loggers { get; }
    }
}
