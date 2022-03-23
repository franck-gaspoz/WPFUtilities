
using System.Collections.Generic;

using Microsoft.Extensions.Hosting;

using SampleApp.Components.ComponentHosts.Hosts;
using SampleApp.Components.ComponentHosts.Hosts.Data;
using SampleApp.Components.ComponentHosts.Loggers;

using WPFUtilities.Components.ServiceComponent;
using WPFUtilities.Extensions.Models;
using WPFUtilities.Extensions.Reflections;
using WPFUtilities.Extensions.Threading;

namespace SampleApp.Components.ComponentHosts.Mediators
{
    public class HostLoggersMediator
    {
        IHostsGridViewModel _hostsGridViewModel;
        ILoggersViewModel _loggersViewModel;

        public HostLoggersMediator(
            HostsComponent hostsGridViewModel,
            ILoggersViewModel loggersViewModel
            )
        {
            _loggersViewModel = loggersViewModel;
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
            _loggersViewModel.Items.Clear();
            var host = _hostsGridViewModel.SelectedItem;
            if (host == null) return;

            foreach (var logger in host.LoggerInformations)
                _loggersViewModel.Items.Add(
                    new LoggerViewModel
                    {
                        GroupName = "Informations",
                        Key = logger.Logger?.ToString(),
                        Value = logger.GetPropertiesDump(
                            new List<string> {
                                nameof(LoggerModel.ProviderType),
                                nameof(LoggerModel.ExternalScope)})
                    });

            foreach (var logger in host.MessageLoggers)
                _loggersViewModel.Items.Add(
                    new LoggerViewModel
                    {
                        GroupName = "Messages",
                        Key = logger.Logger?.ToString(),
                        Value = logger.GetPropertiesDump(
                            new List<string> {
                                nameof(MessageLoggerModel.Category),
                                nameof(MessageLoggerModel.MinLevel)})
                    });

            foreach (var logger in host.ScopeLoggers)
                _loggersViewModel.Items.Add(
                    new LoggerViewModel
                    {
                        GroupName = "Scopes",
                        Key = logger.Logger?.ToString(),
                        Value = logger.GetPropertiesDump(
                            new List<string> {
                                nameof(ScopeLoggerModel.ExternalScopeProvider) })
                    });
        }
    }
}
