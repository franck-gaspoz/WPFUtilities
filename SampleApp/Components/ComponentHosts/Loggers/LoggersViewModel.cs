using SampleApp.Components.Data.KeyValue;

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
