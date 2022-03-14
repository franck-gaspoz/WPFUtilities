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

        IHostViewModel _selectedItem;
        public IHostViewModel SelectedItem
        {
            get => _selectedItem;
            set
            {
                _selectedItem = value;
                NotifyPropertyChanged();
            }
        }

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
                        System.Diagnostics.Debug.WriteLine($" -2-> {host} == {host.ComponentHost.Name} > {child.ComponentHost.Name}");
                        GetHost(child);
                        break;
                }
            }

            Items.Add(host);
            System.Diagnostics.Debug.WriteLine($"-1-> {host} == {host.ComponentHost.Name}");
            host.Childs.ListChanged += ChildHosts_ListChanged;
            GetHosts(host.Childs);
        }
    }
}
