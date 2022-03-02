using System.ComponentModel;

using Microsoft.Extensions.Configuration;

using SampleApp.Components.Settings.Data;

using WPFUtilities.Extensions.Configuration;

namespace SampleApp.Components.Settings.Mediators
{
    public class DataProviderMediator
    {
        ISettingsViewModel _settingsViewModel;
        IDataViewModel _dataViewModel;

        public DataProviderMediator(
            ISettingsViewModel settingsViewModel,
            IDataViewModel dataViewModel)
        {
            _settingsViewModel = settingsViewModel;
            _dataViewModel = dataViewModel;
            settingsViewModel.PropertyChanged += SettingsViewModel_PropertyChanged;
        }

        private void SettingsViewModel_PropertyChanged(
            object sender,
            PropertyChangedEventArgs e)
        {
            if (e.PropertyName != nameof(ISettingsViewModel.SelectedProvider)) return;
            _dataViewModel.Items.Clear();
            dynamic item = _settingsViewModel.SelectedProvider;
            object oprovider = item.Provider;
            if (oprovider is IConfigurationProvider provider
                && provider.TryGetData(out var data))
            {
                foreach (var kvp in data)
                {
                    _dataViewModel.Items.Add(
                        new DataItem { Key = kvp.Key, Value = kvp.Value }
                        );
                }
            }
        }
    }
}
