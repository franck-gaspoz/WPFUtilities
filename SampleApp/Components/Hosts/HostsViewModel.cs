using System.ComponentModel;

using WPFUtilities.ComponentModels;
using WPFUtilities.Components.ServiceComponent;
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

            appHost.ChildHostsCollectionChangedEvent += (o, e) =>
            {
                SetupHosts(appHost);
            };
            SetupHosts(appHost);
        }

        void SetupHosts(IComponentHost host)
        {
            Hosts.Clear();
            GetHosts(host);
            var item = Hosts[0];
            Label = item.Name;
        }

        void GetHosts(IComponentHost host, IHostViewModel parentViewModel = null)
        {
            var item = new HostViewModel();
            item.Initialize(host);

            foreach (var subHost in host.ChildHosts)
                GetHosts(subHost, item);

            if (parentViewModel == null)
                Hosts.Add(item);
            else
                parentViewModel.Hosts.Add(item);
        }
    }
}
