using System.ComponentModel;

using WPFUtilities.ComponentModels;
using WPFUtilities.Components.ServiceComponent;

namespace SampleApp.Components.Hosts
{
    public interface IHostViewModel : IModelBase
    {
        /// <summary>
        /// name
        /// </summary>
        string Name { get; set; }

        /// <summary>
        /// loggers
        /// </summary>
        BindingList<IHostViewModel> Hosts { get; }

        /// <summary>
        /// initialize from a component host
        /// </summary>
        /// <param name="host"></param>
        /// <returns></returns>
        IHostViewModel Initialize(IComponentHost host);
    }
}
