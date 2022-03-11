﻿using System.ComponentModel;
using System.Diagnostics;

using WPFUtilities.ComponentModels;
using WPFUtilities.Components.ServiceComponent;
using WPFUtilities.Extensions.App;

namespace SampleApp.Components.Hosts
{
    /// <summary>
    /// hosts view model
    /// </summary>
    [DebuggerDisplay("{Label}")]
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

            foreach (var subHost in host.ChildHosts)
                GetHosts(subHost, item, level + 1);

            host.ChildHosts.ListChanged += (o, listChanged) =>
            {
                if (!(o is BindingList<IComponentHost> col)) return;
                if (listChanged.ListChangedType == ListChangedType.ItemAdded)
                {
                    var parentVM = item;
                    var newItem = GetHostViewModel(
                        col[listChanged.NewIndex],
                        level + 1,
                        parentVM);
                    item.Childs.Add(newItem);
                }
            };

            if (parentViewModel == null)
                Hosts.Add(item);
            else
                parentViewModel.Childs.Add(item);
        }

        private void ChildHosts_ListChanged(object sender, ListChangedEventArgs e)
        {
            throw new System.NotImplementedException();
        }
    }
}
