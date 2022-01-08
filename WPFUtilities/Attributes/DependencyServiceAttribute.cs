using System;

namespace WPFUtilities.Attributes
{
    /// <summary>
    /// dependency service attributes: allow a class to be auto registered in dependency injector
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public sealed class DependencyServiceAttribute : Attribute
    {
        /// <summary>
        /// dependency scope
        /// </summary>
        public DependencyScope DependencyScope { get; private set; }

        /// <summary>
        /// implementation factory function (optionnal)
        /// </summary>
        public Func<IServiceProvider, object> ImplementationFactory { get; private set; }

        /// <summary>
        /// register a singleton dependency for interface {IClassName} and class {ClassName}
        /// </summary>
        public DependencyServiceAttribute()
        {
            DependencyScope = DependencyScope.Singleton;
        }

        /// <summary>
        /// register dependecy for specified scope and for interface {IClassName} and class {ClassName}
        /// </summary>
        /// <param name="dependencyScope">dependency scope</param>
        public DependencyServiceAttribute(DependencyScope dependencyScope)
        {
            DependencyScope = dependencyScope;
        }
    }
}
