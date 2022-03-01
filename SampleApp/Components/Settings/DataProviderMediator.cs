using System.Collections.Generic;
using System.ComponentModel;

using WPFUtilities.Extensions.Reflections;

namespace SampleApp.Components.Settings
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
            dynamic item = _settingsViewModel.SelectedProvider;
            object provider = item.Provider;
            if (provider.GetProperty<Dictionary<string, string>>("Data", out var data))
            {
                _dataViewModel.Items.Clear();
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
