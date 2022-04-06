using WPFUtilities.Components.UI.KeyValueDataGridControl;

namespace SampleApp.Components.ComponentHosts.Loggers
{
    /// <summary>
    /// loggers view model
    /// </summary>
    public class LoggersViewModel :
        KeyValueViewModel<ILoggerViewModel>,
        ILoggersViewModel
    {
    }
}
