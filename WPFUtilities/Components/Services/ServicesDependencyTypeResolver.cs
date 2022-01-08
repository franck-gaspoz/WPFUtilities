using System;

namespace WPFUtilities.Components.Services
{
    /// <summary>
    /// services dependency type resolver, implements wpf usual naming conventions
    /// </summary>
    public class ServicesDependencyTypeResolver : IServicesDependencyTypeResolver
    {
        /// <inheritdoc/>
        public Type GetDirectInterfaceType(Type type)
        {
            var interfaceTypeName = type.Namespace + "." + "I" + type.Name;
            return type.GetInterface(interfaceTypeName);
        }

        /// <summary>
        /// get a view model interface type for a view
        /// </summary>
        /// <param name="type">view type</param>
        /// <returns>{Name}View -> I{Name}ViewModel if exists, null otherwize</returns>
        public Type GetViewToViewModelInterfaceType(Type type)
        {
            var viewModelTypeName =
                type.Namespace
                + ".I"
                + type.Name + "Model";

            var resolvedType = type
                .Assembly
                .GetType(viewModelTypeName);
            if (resolvedType != null && !resolvedType.IsInterface)
                return null;
            return resolvedType;
        }

        /// <summary>
        /// get a view model interface type for a prefixless view
        /// </summary>
        /// <param name="type">view type</param>
        /// <returns>{Name} -> I{Name}ViewModel if exists, null otherwize</returns>
        public Type GetTypeToViewModelInterfaceType(Type type)
        {
            var viewModelTypeName =
                type.Namespace
                + ".I"
                + type.Name + "ViewModel";

            var resolvedType = type
                .Assembly
                .GetType(viewModelTypeName);
            if (resolvedType != null && !resolvedType.IsInterface)
                return null;
            return resolvedType;
        }

        /// <summary>
        /// try to find a view model interface type that match naming conventions with given type. returns null if not found
        /// </summary>
        /// <param name="type"></param>
        /// <returns>GetViewToViewModelInterfaceType || GetTypeToViewModelInterfaceType || null</returns>
        public Type GetViewModelInterfaceType(Type type)
        {
            var interfaceType = GetViewToViewModelInterfaceType(type);
            if (interfaceType == null)
                interfaceType = GetTypeToViewModelInterfaceType(type);
            return interfaceType;
        }
    }
}
