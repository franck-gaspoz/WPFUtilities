using Microsoft.Extensions.Logging;

namespace SampleApp.Components.ComponentHosts.Hosts.Data
{
    /// <summary>
    /// message logger model
    /// </summary>
    public class MessageLoggerModel
    {
        public string Category { get; set; }
        public ILogger Logger { get; set; }
        public string LoggerDescription { get; set; }
        public LogLevel? MinLevel { get; set; }
    }
}
