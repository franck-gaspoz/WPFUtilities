
using Microsoft.Extensions.Logging;

namespace SampleApp.Components.ComponentHosts.Hosts.Data
{
    /// <summary>
    /// scope logger model
    /// </summary>
    public class ScopeLoggerModel
    {
        public IExternalScopeProvider ExternalScopeProvider { get; set; }
        public ILogger Logger { get; set; }
        public string LoggerDescription { get; set; }
    }
}
