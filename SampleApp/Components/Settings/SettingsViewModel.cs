
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

using SampleApp.Components.Settings.Providers;

using WPFUtilities.ComponentModels;
using WPFUtilities.Extensions.Configuration;
using WPFUtilities.Extensions.Reflections;

namespace SampleApp.Components.Settings
{
    public class SettingsViewModel
        : ModelBase,
        ISettingsViewModel
    {
        #region properties

        string _environmentName = "";
        /// <inheritdoc/>
        public string EnvironmentName
        {
            get
            {
                return _environmentName;
            }
            set
            {
                _environmentName = value;
                NotifyPropertyChanged();
            }
        }

        string _applicationName = "";
        /// <inheritdoc/>
        public string ApplicationName
        {
            get
            {
                return _applicationName;
            }
            set
            {
                _applicationName = value;
                NotifyPropertyChanged();
            }
        }

        string _contentRootPath = "";
        /// <inheritdoc/>
        public string ContentRootPath
        {
            get
            {
                return _contentRootPath;
            }
            set
            {
                _contentRootPath = value;
                NotifyPropertyChanged();
            }
        }

        object _selectedProvider = null;
        /// <inheritdoc/>
        public object SelectedProvider
        {
            get
            {
                return _selectedProvider;
            }
            set
            {
                _selectedProvider = value;
                NotifyPropertyChanged();
            }
        }

        /// <inheritdoc/>
        public BindingList<object> Providers { get; } = new BindingList<object>();

        #endregion

        IHostEnvironment _hostEnvironment;
        IConfiguration _configuration;

        public SettingsViewModel(
            IHostEnvironment hostEnvironment,
            IConfiguration configuration
            )
        {
            _hostEnvironment = hostEnvironment;
            _configuration = configuration;
            Initialize();
        }

        void Initialize()
        {
            EnvironmentName = _hostEnvironment.EnvironmentName;
            ApplicationName = _hostEnvironment.ApplicationName;
            ContentRootPath = _hostEnvironment.ContentRootPath;
            Providers.Clear();

            if (_configuration.GetField<List<IConfigurationProvider>>("_providers", out var providers))
                AppendProviders(providers);
        }

        void AppendProviders(List<IConfigurationProvider> providers, string padLeft = "")
        {
            foreach (var provider in providers)
            {
                var name = padLeft + provider.ToString();
                var providerItemViewModel = new ProviderItemViewModel
                {
                    Name = name,
                    Provider = provider
                };

                if (provider.TryGetData(out var data))
                    providerItemViewModel.DataCount = data.Count;
                if (provider is FileConfigurationProvider fileConfigurationProvider)
                    providerItemViewModel.IsProvidingFile =
                        !string.IsNullOrEmpty(fileConfigurationProvider.Source?.Path);

                Providers.Add(providerItemViewModel);

                if (provider is ChainedConfigurationProvider chained)
                {
                    if (chained.GetField<ConfigurationRoot>("_config", out var config))
                        AppendProviders(config.Providers.ToList(), padLeft + "    ");
                }
            }
        }
    }
}
