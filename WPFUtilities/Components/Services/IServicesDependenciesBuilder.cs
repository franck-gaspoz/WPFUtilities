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
        /// instantiate and call dependency services initializers classes (having interface IDependencyServiceInitializer)
        /// </summary>
        /// <returns>the services dependencies builder that has been called</returns>
        ServicesDependenciesBuilder AddDependencyServicesInitializers();

        /// <summary>
        /// instantiate and call dependency services initializers classes (having interface IDependencyServiceInitializer) in the given assembly
        /// </summary>
        /// <param name="assembly">assembly</param>
        /// <returns>the services dependencies builder that has been called</returns>
        ServicesDependenciesBuilder AddDependencyServicesInitializers(Assembly assembly);

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

    }
}