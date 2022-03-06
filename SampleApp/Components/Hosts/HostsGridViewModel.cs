using System.Collections.Generic;
using System.ComponentModel;

using WPFUtilities.ComponentModels;

namespace SampleApp.Components.Hosts
{
    public class HostsGridViewModel : ModelBase, IHostsGridViewModel
    {
        public BindingList<IHostViewModel> Items { get; }
            = new BindingList<IHostViewModel>();

        IHostsViewModel _hostsViewModel;

        public HostsGridViewModel(
            IHostsViewModel hostsViewModel
            )
        {
            _hostsViewModel = hostsViewModel;
            _hostsViewModel.PropertyChanged += (o, e) => Initialize();
            Initialize();
        }

        void Initialize()
        {
            Items.Clear();
            GetHosts(_hostsViewModel.Hosts);
        }

        void GetHosts(IEnumerable<IHostViewModel> hosts)
        {
            foreach (var host in hosts)
            {
                GetHost(host);
                GetHosts(host.Childs);
            }
        }

        void GetHost(IHostViewModel host)
        {
            Items.Add(host);
        }
    }
}
