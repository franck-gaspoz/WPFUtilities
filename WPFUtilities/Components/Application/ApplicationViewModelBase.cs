
using WPFUtilities.ComponentModels;

namespace WPFUtilities.Components.Application
{
    /// <summary>
    /// application base view model
    /// </summary>
    public class ApplicationViewModelBase : ModelBase, IApplicationViewModelBase
    {
        bool _isBuzy = false;
        /// <summary>
        /// application is buzy (may freeze any command)
        /// </summary>
        public bool IsBuzy
        {
            get
            {
                return _isBuzy;
            }
            set
            {
                _isBuzy = value;
                NotifyPropertyChanged();
            }
        }
    }
}
