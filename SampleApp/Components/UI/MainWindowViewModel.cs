
using System.Reflection;

using WPFUtilities.Attributes;
using WPFUtilities.ComponentModels;
using WPFUtilities.Components.Appl;

namespace SampleApp.Components.UI
{
    /// <summary>
    /// main window view model
    /// </summary>
    [DependencyService]
    public class MainWindowViewModel : ModelBase, IModelBase, IMainWindowViewModel
    {
        /// <inheritdoc/>
        public string Title
        {
            get
            {
                var wpfuVersion = Assembly.GetAssembly(typeof(ApplicationBase)).GetName().Version;
                return $"WPFUtilities samples application | WPFUtilites v{wpfuVersion}";
            }
        }
    }
}
