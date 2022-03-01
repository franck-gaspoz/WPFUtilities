using System;
using System.Linq;
using System.Reflection;

namespace WPFUtilities.Extensions.Reflections
{
    /// <summary>
    /// types extensions
    /// </summary>
    public static class TypeExtensions
    {
        /// <summary>
        /// try to gives the value of the field of an object, whether it is public,protected or private
        /// </summary>
        /// <typeparam name="T">expected value type</typeparam>
        /// <param name="obj">object</param>
        /// <param name="fieldName">field name</param>
        /// <param name="fieldValue">out field value</param>
        /// <returns>true if value found with expected type, false otherwise</returns>
        public static bool GetField<T>(this object obj, string fieldName, out T fieldValue)
            where T : class
        {
            fieldValue = default(T);
            if (obj == null) return false;

            var fieldInfo = obj.GetType().GetField(fieldName,
                BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static);
            if (fieldInfo == null) throw new InvalidOperationException($"field '{fieldName}' not found");

            fieldValue = fieldInfo.GetValue(obj) as T;
            return fieldValue is T;
        }

        /// <summary>
        /// try to gives the value of the property of an object, whether it is public,protected or private
        /// </summary>
        /// <typeparam name="T">expected value type</typeparam>
        /// <param name="obj">object</param>
        /// <param name="propertyName">field name</param>
        /// <param name="propertyValue">out field value</param>
        /// <returns>true if value found with expected type, false otherwise</returns>
        public static bool GetProperty<T>(this object obj, string propertyName, out T propertyValue)
            where T : class
        {
            propertyValue = default(T);
            if (obj == null) return false;

            var propInfo = obj.GetType().GetProperty(propertyName,
                BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static);
            if (propInfo == null) throw new InvalidOperationException($"property '{propertyName}' not found");

            propertyValue = propInfo.GetValue(obj) as T;
            return propertyValue is T;
        }

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
