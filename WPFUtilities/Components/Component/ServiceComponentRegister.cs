using System.Collections.Generic;

namespace WPFUtilities.Components.Component
{
    /// <summary>
    /// register of services components
    /// </summary>
    public sealed class ServiceComponentRegister
        : IServiceComponentRegister
    {
        /// <summary>
        /// components with an identifier (multitons)
        /// </summary>
        readonly Dictionary<string, IServiceComponent> _identifiedComponents = new Dictionary<string, IServiceComponent>();

        /// <summary>
        /// components without an identifier (singleton and transients)
        /// </summary>
        readonly IList<IServiceComponent> _anonymousComponents = new List<IServiceComponent>();



    }
}
