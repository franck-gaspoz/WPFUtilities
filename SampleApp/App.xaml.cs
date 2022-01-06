
using WPFUtilities.Components.Appl;

namespace SampleApp
{
    /// <summary>
    /// Application interactor
    /// </summary>
    public partial class App : ApplicationBase
    {
        /// <summary>
        /// creates a new instance, start the app, build and display window
        /// </summary>
        public App()
        {
            var settings = new ApplicationBaseSettings
            {
                MainWindowType = typeof(MainWindow),
                Initialize = () => Initialize()
            };
        }

        void Initialize()
        {

        }
    }
}
