
using SampleApp.Components.UI;

using WPFUtilities.Components.Application;

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
        /// creates a new instance, starts the app, build and display window
        /// </summary>
        public App() : base(new ApplicationBaseSettings
        {
            MainWindowType = typeof(MainWindow),
            MainWindowComponentType = typeof(MainWindowComponent)
        })
        { }
    }
}
