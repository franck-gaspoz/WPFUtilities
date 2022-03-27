
using System.IO;

using Microsoft.Extensions.Hosting;

using SampleApp.Components.ComponentHosts.Hosts;
using SampleApp.Components.ComponentHosts.Services;

using WPFUtilities.Components.ServiceComponent;
using WPFUtilities.Extensions.Models;
using WPFUtilities.Extensions.Reflections;
using WPFUtilities.Extensions.Threading;

namespace SampleApp.Components.ComponentHosts.Mediators
{
    public class HostServicesRegisteredMediator
    {
        IHostsGridViewModel _hostsGridViewModel;
        IServicesRegisteredViewModel _servicesViewModel;

        public HostServicesRegisteredMediator(
            HostsComponent hostsGridViewModel,
            IServicesRegisteredViewModel servicesViewModel
            )
        {
            _servicesViewModel = servicesViewModel;
            hostsGridViewModel.ComponentHost
                .OnNotNull<IHost>(
                    nameof(IComponentHost.Host),
                    (host) =>
                    {
                        _hostsGridViewModel =
                            hostsGridViewModel.ComponentHost
                                .Services
                                .GetRequiredService<IHostsGridViewModel>();

                        _hostsGridViewModel
                            .OnPropertyChanged<IHostsGridViewModel>(
                                nameof(IHostsGridViewModel.SelectedItem),
                                (model, changed) =>
                                {
                                    Initialize();
                                });
                    });
        }

        void Initialize()
        {
            _servicesViewModel.Items.Clear();
            var host = _hostsGridViewModel.SelectedItem;
            if (host == null) return;

            foreach (var service in host.RegisteredServices)
            {
                _servicesViewModel.Items.Add(
                    new ServiceViewModel
                    {
                        GroupName = null,
                        Key = service.RegisteredType.UnmangledTypeName(true),
                        Value = Path.GetFileNameWithoutExtension(service.Assembly.Location)
                    });
            }
        }
    }
}
