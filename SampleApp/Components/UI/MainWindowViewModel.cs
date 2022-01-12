
using System.Reflection;

using WPFUtilities.ComponentModels;
using WPFUtilities.Components.Appl;
using WPFUtilities.Components.Services;

namespace SampleApp.Components.UI
{
    /// <summary>
    /// main window view model
    /// </summary>
    [ServiceDependency]
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
