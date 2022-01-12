
using SampleApp.Components.UI;

using WPFUtilities.Components.Appl;

namespace SampleApp
{
    /// <summary>
    /// Application interactor
    /// </summary>
    public partial class App :
        ApplicationBase,
        IApplicationBase
    {
        /// <summary>
        /// creates a new instance, start the app, build and display window
        /// </summary>
        public App()
        {
            ApplicationBaseSettings = new ApplicationBaseSettings
            {
                MainWindowType = typeof(MainWindow),
                MainWindowComponentType = typeof(MainWindowComponent)
            };
        }
    }
}
