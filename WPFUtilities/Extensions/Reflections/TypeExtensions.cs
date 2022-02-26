using System;
using System.Linq;

namespace WPFUtilities.Extensions.Reflections
{
    /// <summary>
    /// types extensions
    /// </summary>
    public static class TypeExtensions
    {
        /// <summary>
        /// indicates if a type inherits from a type having given name. returns false if type has given type name
        /// </summary>
        /// <param name="type">type to analyze</param>
        /// <param name="typeName">parent type</param>
        /// <returns>true if type inherits from type name, false otherwize</returns>
        public static bool InheritsFrom(this Type type, string typeName)
        {
            if (type.Name == typeName) return false;
            while (type != null && type != typeof(object))
            {
                if (type.Name == typeName) return true;
                type = type.BaseType;
            }
            return false;
        }

        /// <summary>
        /// indicates if a type inherits from a type having an interface with the given interface name. returns true if type has given interface type name
        /// </summary>
        /// <param name="type">type to analyze</param>
        /// <param name="interfaceTypeName">interface type name</param>
        /// <returns>true if type implements an interface, false otherwize</returns>
        public static bool HasInterface(this Type type, string interfaceTypeName)
            => type.GetInterfaces().Where(x => x.FullName == interfaceTypeName).Any();

        /// <summary>
        /// indicates if a type inherits from a type having an interface with the given interface name. returns true if type has given interface type name
        /// </summary>
        /// <param name="type">type to analyze</param>
        /// <typeparam name="InterfaceType">interface type</typeparam>
        /// <returns>true if type implements an interface, false otherwize</returns>
        public static bool HasInterface<InterfaceType>(this Type type)
            => HasInterface(type, typeof(InterfaceType).FullName);
    }
}
