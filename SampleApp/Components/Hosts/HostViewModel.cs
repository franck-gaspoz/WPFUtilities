using System.ComponentModel;

using WPFUtilities.ComponentModels;
using WPFUtilities.Components.ServiceComponent;

namespace SampleApp.Components.Hosts
{
    /// <summary>
    /// hosts view model
    /// </summary>
    public class HostViewModel : ModelBase, IHostViewModel
    {
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

        /// <summary>
        /// hosts
        /// </summary>
        public BindingList<IHostViewModel> Hosts { get; }
            = new BindingList<IHostViewModel>();

        /// <inheritdoc/>
        public IHostViewModel Initialize(IComponentHost host)
        {
            Name = host.Name;
            return this;
        }

    }
}
