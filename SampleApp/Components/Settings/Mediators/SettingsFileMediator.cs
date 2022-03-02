using System;
using System.IO;

using Microsoft.Extensions.Configuration.Json;
using Microsoft.Extensions.Logging;

using SampleApp.Components.Settings.Files;

using WPFUtilities.Extensions.Exceptions;
using WPFUtilities.Extensions.Reflections;

namespace SampleApp.Components.Settings.Mediators
{
    /// <summary>
    /// file view model mediator
    /// </summary>
    public class SettingsFileMediator
    {
        ISettingsViewModel _settingsViewModel;
        ISettingsFileViewModel _settingsFileViewModel;
        ILogger<SettingsFileMediator> _logger;

        public SettingsFileMediator(
            ISettingsViewModel settingsViewModel,
            ISettingsFileViewModel settingsFileViewModel,
            ILogger<SettingsFileMediator> logger)
        {
            _logger = logger;
            _settingsViewModel = settingsViewModel;
            _settingsFileViewModel = settingsFileViewModel;
            settingsViewModel.PropertyChanged += SettingsViewModel_PropertyChanged;
        }

        void ClearModel()
        {
            _settingsFileViewModel.Error =
            _settingsFileViewModel.FileName =
            _settingsFileViewModel.FilePath =
            _settingsFileViewModel.FileText = null;
        }

        private void SettingsViewModel_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName != nameof(ISettingsViewModel.SelectedProvider)) return;
            ClearModel();
            dynamic item = _settingsViewModel.SelectedProvider;
            object provider = item.Provider;
            if (provider is JsonConfigurationProvider jsonProvider)
            {
                _settingsFileViewModel.IsVisible = true;
                try
                {
                    _settingsFileViewModel.FileName = jsonProvider.Source.Path;
                    if (jsonProvider.Source.FileProvider.GetProperty<string>("Root", out var root))
                    {
                        var path = Path.Combine(root, _settingsFileViewModel.FileName);
                        _settingsFileViewModel.FileName = Path.GetFileName(path);
                        _settingsFileViewModel.FilePath = Path.GetDirectoryName(path);
                    }
                    _settingsFileViewModel.FileText =
                        File.ReadAllText(_settingsFileViewModel.FullPath);
                }
                catch (Exception fileLoadError)
                {
                    _settingsFileViewModel.Error = fileLoadError.GetFullMessage();
                    _logger.LogError(fileLoadError, _settingsFileViewModel.Error);
                }
            }
            else
                _settingsFileViewModel.IsVisible = false;
        }
    }
}
