using System;

namespace WPFUtilities.Components.Services
{
    /// <summary>
    /// services dependency type resolver
    /// </summary>
    public interface IServicesDependencyTypeResolver
    {
        /// <summary>
        /// get an interface type owned by the type named according to naming conventions. returns null if not found
        /// <para>{Name} -> I{Name}</para>
        /// </summary>
        /// <param name="type">type to lookup for correctly named interface</param>
        /// <returns>interface type according to naming conventions if any, null otherwize</returns>
        Type GetDirectInterfaceType(Type type);

        /// <summary>
        /// get a view model interface type for a view
        /// </summary>
        /// <param name="type">view type</param>
        /// <returns>{Name}View -> I{Name}ViewModel if exists, null otherwize</returns>
        Type GetViewToViewModelInterfaceType(Type type);

        /// <summary>
        /// get a view model interface type for a prefixless view
        /// </summary>
        /// <param name="type">view type</param>
        /// <returns>{Name} -> I{Name}ViewModel if exists, null otherwize</returns>
        Type GetTypeToViewModelInterfaceType(Type type);

        /// <summary>
        /// try to find a view model interface type that match naming conventions with given type. returns null if not found
        /// </summary>
        /// <param name="type"></param>
        /// <returns>GetViewToViewModelInterfaceType || GetTypeToViewModelInterfaceType || null</returns>
        Type GetViewModelInterfaceType(Type type);
    }
}