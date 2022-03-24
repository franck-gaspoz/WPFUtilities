using System;

using Microsoft.Extensions.Logging;

namespace SampleApp.Components.ComponentHosts.Hosts.Data
{
    /// <summary>
    /// logger model
    /// </summary>
    public class LoggerModel : TypeReferenceModelAbstract
    {
        public string Category { get; set; }

        public bool ExternalScope { get; set; }

        public ILogger Logger { get; set; }

        public string LoggerDescription { get; set; }

        public Type ProviderType { get; set; }
    }
}
