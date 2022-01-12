using System;

namespace WPFUtilities.Components.Services
{
    /// <summary>
    /// dependency service attributes: allow a class to be auto registered in dependency injector
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public sealed class ServiceDependencyAttribute : Attribute
    {
        /// <summary>
        /// dependency scope
        /// </summary>
        public DependencyScope DependencyScope { get; private set; }

        /// <summary>
        /// register dependecy for specified scope and eventually an implementation factory
        /// </summary>
        /// <param name="dependencyScope">dependency scope (default singleon)</param>
        public ServiceDependencyAttribute(
            DependencyScope dependencyScope = DependencyScope.Singleton
            )
        {
            DependencyScope = dependencyScope;
        }
    }
}
