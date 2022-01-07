using System;
using System.Reflection;

namespace WPFUtilities.Components.Services
{
    /// <summary>
    /// find services dependencies and add to host services collection
    /// </summary>
    public interface IServicesDependenciesBuilder
    {
        /// <summary>
        /// add singleton services to service collection
        /// </summary>
        /// <returns>the services dependencies builder that has been called</returns>
        ServicesDependenciesBuilder AddSingletonServices();

        /// <summary>
        /// add classes with dependency service attribute to service collection
        /// </summary>
        /// <returns>the services dependencies builder that has been called</returns>
        ServicesDependenciesBuilder AddDependencyServices();

        /// <summary>
        /// add singleton services to service collection from types in the given assembly
        /// </summary>
        /// <param name="assembly">assembly</param>
        /// <returns>the services dependencies builder that has been called</returns>
        ServicesDependenciesBuilder AddSingletonServices(Assembly assembly);

        /// <summary>
        /// add classes with dependency service attribute to service collection from types in the given assembly
        /// </summary>
        /// <param name="assembly">assembly</param>
        /// <returns>the services dependencies builder that has been called</returns>
        ServicesDependenciesBuilder AddDependencyServices(Assembly assembly);

        /// <summary>
        /// get an interface type owned by the type named according to naming conventions. returns null if not found
        /// <para>{Name} -> I{Name}</para>
        /// </summary>
        /// <param name="type">type to lookup for correctly named interface</param>
        /// <returns>interface type according to naming conventions if any, null otherwize</returns>
        Type GetDirectInterfaceType(Type type);
    }
}