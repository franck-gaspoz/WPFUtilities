using System.Collections.Generic;
using System.ComponentModel;

using WPFUtilities.ComponentModels;

namespace SampleApp.Components.Hosts
{
    /// <summary>
    /// flat project the hosts view model (Hosts -> Items)
    /// </summary>
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
            void ChildHosts_ListChanged(object o, ListChangedEventArgs e)
            {
                switch (e.ListChangedType)
                {
                    case ListChangedType.Reset:
                        Initialize();
                        break;
                    case ListChangedType.ItemAdded:
                        GetHost(host.Childs[e.NewIndex]);
                        break;
                }
            }

            Items.Add(host);
            host.Childs.ListChanged += ChildHosts_ListChanged;
        }
    }
}
