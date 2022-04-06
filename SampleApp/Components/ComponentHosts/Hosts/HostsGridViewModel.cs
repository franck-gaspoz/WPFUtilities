using System.Collections.Generic;
using System.ComponentModel;

using WPFUtilities.Components.UI.TreeDataGridControl;

namespace SampleApp.Components.ComponentHosts.Hosts
{
    /// <summary>
    /// flat project the hosts view model (Hosts -> Items)
    /// </summary>
    public class HostsGridViewModel :
        TreeDataGridViewModel<IHostViewModel>,
        IHostsGridViewModel
    {
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
                GetHost(host);
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
                        var child = host.Childs[e.NewIndex];
                        GetHost(child);
                        break;
                }
            }

            Items.Add(host);
            host.Childs.ListChanged += ChildHosts_ListChanged;
            GetHosts(host.Childs);
        }
    }
}
