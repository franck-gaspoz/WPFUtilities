
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

using WPFUtilities.ComponentModels;
using WPFUtilities.Extensions.Reflections;

namespace SampleApp.Components.Settings
{
    public class SettingsViewModel
        : ModelBase,
        ISettingsViewModel
    {
        #region properties

        string _environmentName = "";
        /// <summary>
        /// environment name
        /// </summary>
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
        /// <summary>
        /// application name
        /// </summary>
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
        /// <summary>
        /// content root path
        /// </summary>
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

        /// <summary>
        /// providers
        /// </summary>
        public BindingList<string> Providers { get; } = new BindingList<string>();

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
            InitializeViewModel();
        }

        void InitializeViewModel()
        {
            EnvironmentName = _hostEnvironment.EnvironmentName;
            ApplicationName = _hostEnvironment.ApplicationName;
            ContentRootPath = _hostEnvironment.ContentRootPath;

            if (_configuration.GetField<List<IConfigurationProvider>>("_providers", out var providers))
                AppendProviders(providers);
        }

        void AppendProviders(List<IConfigurationProvider> providers, string padLeft = "")
        {
            foreach (var provider in providers)
            {
                var name = padLeft + provider.ToString();
                Providers.Add(name);
                if (provider is ChainedConfigurationProvider chained)
                {
                    if (chained.GetField<ConfigurationRoot>("_config", out var config))
                        AppendProviders(config.Providers.ToList(), padLeft + "    ");
                }
            }
        }
    }
}
