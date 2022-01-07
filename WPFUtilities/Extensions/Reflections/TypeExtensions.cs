using System;

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
    }
}
