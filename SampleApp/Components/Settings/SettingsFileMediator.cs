using Microsoft.Extensions.Configuration.Json;

namespace SampleApp.Components.Settings
{
    /// <summary>
    /// file mediator
    /// </summary>
    public class SettingsFileMediator
    {
        ISettingsViewModel _settingsViewModel;
        ISettingsFileViewModel _settingsFileViewModel;

        public SettingsFileMediator(
            ISettingsViewModel settingsViewModel,
            ISettingsFileViewModel settingsFileViewModel)
        {
            _settingsViewModel = settingsViewModel;
            _settingsFileViewModel = settingsFileViewModel;
            settingsViewModel.PropertyChanged += SettingsViewModel_PropertyChanged; ;
        }

        private void SettingsViewModel_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName != nameof(ISettingsViewModel.SelectedProvider)) return;
            dynamic item = _settingsViewModel.SelectedProvider;
            object provider = item.Provider;
            if (provider is JsonConfigurationProvider jsonProvider)
            {
                _settingsFileViewModel.IsVisible = true;
            }
            else
                _settingsFileViewModel.IsVisible = false;
        }
    }
}
