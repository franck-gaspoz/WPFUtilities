using System.ComponentModel;

using SampleApp.Components.Data;

namespace SampleApp.Components.Loggers.Mediators
{
    public class DataProviderMediator
    {
        ILoggersViewModel _LoggersViewModel;
        IDataViewModel _dataViewModel;

        public DataProviderMediator(
            ILoggersViewModel LoggersViewModel,
            IDataViewModel dataViewModel
            )
        {
            _LoggersViewModel = LoggersViewModel;
            _dataViewModel = dataViewModel;
            LoggersViewModel.PropertyChanged += LoggersViewModel_PropertyChanged;
        }

        private void LoggersViewModel_PropertyChanged(
            object sender,
            PropertyChangedEventArgs e)
        {
            if (e.PropertyName != nameof(ILoggersViewModel.SelectedLogger)) return;
            _dataViewModel.Items.Clear();
            dynamic item = _LoggersViewModel.SelectedLogger;
            object oprovider = item.Provider;

            /*var items = new List<DataItem>();

            if (oprovider is IConfigurationProvider provider
                && provider.TryGetData(out var data))
            {
                foreach (var kvp in data)
                {
                    items.Add(
                        new DataItem { Key = kvp.Key, Value = kvp.Value }
                        );
                }
                foreach (var dataItem in items)
                    _dataViewModel.Items.Add(dataItem);
            }
            _dataViewModel.NotifyPropertyChanged(nameof(IDataViewModel.Items));*/
        }
    }
}
