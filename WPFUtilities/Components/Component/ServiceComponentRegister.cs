using System;
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
        readonly Dictionary<string, Type> _identifiedComponents = new Dictionary<string, Type>();

    }
}
