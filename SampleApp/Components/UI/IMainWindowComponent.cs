
using WPFUtilities.Components.Logging.ListLogger;
using WPFUtilities.Components.ServiceComponent;

namespace SampleApp.Components.UI
{
    public interface IMainWindowComponent : IServiceComponent
    {
        /// <summary>
        /// window list logger model
        /// </summary>
        IListLoggerModel ListLoggerModel { get; }
    }
}
