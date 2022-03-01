using WPFUtilities.ComponentModels;

namespace SampleApp.Components.Settings
{
    public class SettingsFileViewModel : ModelBase, ISettingsFileViewModel
    {
        bool _isVisible = false;
        /// <inheritdoc/>
        public bool IsVisible
        {
            get
            {
                return _isVisible;
            }
            set
            {
                _isVisible = value;
                NotifyPropertyChanged();
            }
        }
    }
}
