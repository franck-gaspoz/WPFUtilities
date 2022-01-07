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
        ServicesDependenciesBuilder AddSingletonServices();

        /// <summary>
        /// add classes with dependency service attribute to service collection
        /// </summary>
        ServicesDependenciesBuilder AddDependencyServices();

        /// <summary>
        /// add singleton services to service collection from types in the given assembly
        /// </summary>
        /// <param name="assembly">assembly</param>
        ServicesDependenciesBuilder AddSingletonServices(Assembly assembly);

        /// <summary>
        /// add classes with dependency service attribute to service collection from types in the given assembly
        /// </summary>
        /// <param name="assembly">assembly</param>
        ServicesDependenciesBuilder AddDependencyServices(Assembly assembly);
    }
}