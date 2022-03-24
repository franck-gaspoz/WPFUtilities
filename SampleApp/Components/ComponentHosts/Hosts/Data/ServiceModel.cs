using System;

namespace SampleApp.Components.ComponentHosts.Hosts.Data
{
    /// <summary>
    /// service model
    /// </summary>
    public class ServiceModel : TypeReferenceModelAbstract
    {
        public Type RegisteredType { get; set; }

        public Type ResolvedType { get; set; }

        public string Name { get; set; }
    }
}
