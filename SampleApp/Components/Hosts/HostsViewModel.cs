using System.ComponentModel;

using WPFUtilities.ComponentModels;
using WPFUtilities.Extensions.App;

namespace SampleApp.Components.Hosts
{
    /// <summary>
    /// hosts view model
    /// </summary>
    public class HostsViewModel : ModelBase, IHostsViewModel
    {
        string _label = null;
        /// <summary>
        /// label
        /// </summary>
        public string Label
        {
            get
            {
                return _label;
            }
            set
            {
                _label = value;
                NotifyPropertyChanged();
            }
        }

        /// <summary>
        /// hosts
        /// </summary>
        public BindingList<IHostViewModel> Hosts { get; }
            = new BindingList<IHostViewModel>();

        public HostsViewModel()
        {
            Initialize();
        }

        void Initialize()
        {
            var appHost = this.GetApplication().ApplicationHost;
            var hostViewModel = new HostViewModel()
                .Initialize(appHost);
            Hosts.Add(hostViewModel);
            Label = hostViewModel.Name;
        }
    }
}
