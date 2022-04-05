
using System.Reflection;

using WPFUtilities.ComponentModels;
using WPFUtilities.Components.Application;

namespace SampleApp.Components.Main
{
    /// <summary>
    /// main window view model
    /// </summary>
    public class MainWindowViewModel : ModelBase, IModelBase, IMainWindowViewModel
    {
        static int MainWindowViewModelCounter = 0;

        /// <inheritdoc/>
        public string Title
        {
            get
            {
                var wpfuVersion = Assembly.GetAssembly(typeof(ApplicationBase)).GetName().Version;
                return $"WPFUtilities samples | WPFUtilites v{wpfuVersion}";
            }
        }

        int _number = 1;
        /// <inheritdoc/>
        public int Number
        {
            get
            {
                return _number;
            }
            set
            {
                _number = value;
                NotifyPropertyChanged();
            }
        }

        public MainWindowViewModel()
        {
            Number = ++MainWindowViewModelCounter;
        }
    }
}
