using WPFUtilities.ComponentModels;

namespace SampleApp.Components.Data.KeyValue
{
    /// <inheritdoc/>
    public class KeyValueItemViewModel : ModelBase, IKeyValueItem
    {
        string _key = null;
        /// <inheritdoc/>
        public string Key
        {
            get
            {
                return _key;
            }
            set
            {
                _key = value;
                NotifyPropertyChanged();
            }
        }

        string _Value = null;
        /// <inheritdoc/>
        public string Value
        {
            get
            {
                return _Value;
            }
            set
            {
                _Value = value;
                NotifyPropertyChanged();
            }
        }
    }
}
