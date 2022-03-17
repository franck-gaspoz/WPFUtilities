using System.ComponentModel;
using System.Diagnostics;

using WPFUtilities.ComponentModels;
using WPFUtilities.Components.ServiceComponent;
using WPFUtilities.Extensions.App;

namespace SampleApp.Components.Hosts
{
    /// <summary>
    /// hosts view model
    /// </summary>
    [DebuggerDisplay("HostsViewModel: childs count = {Hosts.Count}")]
    public class HostsViewModel : ModelBase, IHostsViewModel
    {
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
            };
            SetupHosts(appHost);
        }

        void SetupHosts(IComponentHost host)
        {
            Hosts.Clear();
            GetHosts(host);
            var item = Hosts[0];
        }

        void GetHosts(
            IComponentHost host,
            IHostViewModel parentViewModel = null,
            int level = 0)
        {
            IHostViewModel GetHostViewModel(IComponentHost ho, int lev, IHostViewModel pvm)
            {
                var r = new HostViewModel();
                r.Initialize(ho, lev, pvm);
                return r;
            }
            var item = GetHostViewModel(host, level, parentViewModel);

            void ChildHosts_ListChanged(object o, ListChangedEventArgs listChanged)
            {
                if (!(o is BindingList<IComponentHost> col)) return;
                if (listChanged.ListChangedType == ListChangedType.ItemAdded)
                {
                    GetHosts(col[listChanged.NewIndex], item, level + 1);
                }
            }

            host.ChildHosts.ListChanged += ChildHosts_ListChanged;

            if (parentViewModel == null)
                Hosts.Add(item);
            else
                parentViewModel.Childs.Add(item);

            foreach (var subHost in host.ChildHosts)
                GetHosts(subHost, item, level + 1);
        }
    }
}
