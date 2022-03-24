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
        /// return unmangled type name
        /// </summary>
        /// <param name="type">type</param>
        /// <param name="fullName">if true builds a long name, else a short</param>
        /// <returns></returns>
        public static string UnmangledTypeName(this Type type, bool fullName = false)
        {
            if (type == null) return null;

            string name = fullName ? type.FullName : type.Name;
            if (!type.IsGenericType) return name;

            name = name.Split('`')[0];
            name += "<";
            int i = 0;
            foreach (Type genericArgumentType in type.GetGenericArguments())
            {
                if (i > 0) name += ",";
                name += UnmangledTypeName(genericArgumentType, fullName);
                i++;
            }
            name += ">";
            return name;
        }

        /// <summary>
        /// default binding scopes
        /// </summary>
        public const BindingFlags DefaultScopeBindingFlags =
            BindingFlags.Public | BindingFlags.NonPublic
            | BindingFlags.Instance | BindingFlags.Static;

        /// <summary>
        /// try to gives the value of the field of an object, whether it is public,protected or private
        /// </summary>
        /// <typeparam name="T">expected value type</typeparam>
        /// <param name="obj">object</param>
        /// <param name="fieldName">field name</param>
        /// <param name="fieldValue">out field value</param>
        /// <returns>true if value found with expected type, false otherwise</returns>
        public static bool GetField<T>(this object obj, string fieldName, out T fieldValue)
        {
            fieldValue = default(T);
            if (obj == null) return false;

            var fieldInfo = obj.GetType().GetField(fieldName,
                DefaultScopeBindingFlags);
            if (fieldInfo == null) return false;

            fieldValue = (T)fieldInfo.GetValue(obj);
            return fieldValue is T;
        }

        /// <summary>
        /// try to gives the value of the field of an object, whether it is public,protected or private
        /// </summary>
        /// <typeparam name="T">expected value type</typeparam>
        /// <param name="obj">object</param>
        /// <param name="methodName">method name</param>
        /// <returns>method invoke result if method is found, null otherwise</returns>
        public static T InvokeMethod<T>(this object obj, string methodName, params object[] parameters)
        {
            var result = default(T);
            if (obj == null) return result;

            var methodInfo = obj.GetType().GetMethod(methodName,
                DefaultScopeBindingFlags);
            if (methodInfo == null) return result;

            result = (T)methodInfo.Invoke(obj, parameters);
            return result;
        }

        /// <summary>
        /// try to gives the value of the field of an object, whether it is public,protected or private
        /// </summary>
        /// <typeparam name="T">expected value type</typeparam>
        /// <param name="obj">object</param>
        /// <param name="fieldName">field name</param>
        /// <returns>null if value not found or null, value of type T otherwise</returns>
        public static T GetField<T>(this object obj, string fieldName)
        {
            var fieldValue = default(T);
            if (obj == null) return fieldValue;

            var fieldInfo = obj.GetType().GetField(fieldName,
                DefaultScopeBindingFlags);
            if (fieldInfo == null) return fieldValue;

            fieldValue = (T)fieldInfo.GetValue(obj);
            return fieldValue;
        }

        /// <summary>
        /// check if an object has a field, whether it is public,protected or private
        /// </summary>
        /// <param name="obj">object</param>
        /// <param name="fieldName">field name</param>
        /// <returns>true if has field</returns>
        public static bool HasField(this object obj, string fieldName)
            => obj.GetType().GetField(fieldName,
                DefaultScopeBindingFlags)
                != null;

        /// <summary>
        /// check if an object has a property, whether it is public,protected or private
        /// </summary>
        /// <param name="obj">object</param>
        /// <param name="propertyName">property name</param>
        /// <returns>true if has property</returns>
        public static bool HasProperty(this object obj, string propertyName)
            => obj.GetType().GetProperty(propertyName,
                DefaultScopeBindingFlags)
                != null;

        /// <summary>
        /// try to gives the value of the property of an object, whether it is public,protected or private
        /// </summary>
        /// <typeparam name="T">expected value type</typeparam>
        /// <param name="obj">object</param>
        /// <param name="propertyName">field name</param>
        /// <param name="propertyValue">out field value</param>
        /// <returns>true if value found with expected type, false otherwise</returns>
        public static bool GetProperty<T>(this object obj, string propertyName, out T propertyValue)
        {
            propertyValue = default(T);
            if (obj == null) return false;

            var propInfo = obj.GetType().GetProperty(propertyName,
                DefaultScopeBindingFlags);
            if (propInfo == null) return false;

            propertyValue = (T)propInfo.GetValue(obj);
            return propertyValue is T;
        }

        /// <summary>
        /// try to gives the value of the property of an object, whether it is public,protected or private
        /// </summary>
        /// <typeparam name="T">expected value type</typeparam>
        /// <param name="obj">object</param>
        /// <param name="propertyName">field name</param>
        /// <returns>null if value not found or null, value of type T otherwise</returns>
        public static T GetProperty<T>(this object obj, string propertyName)
        {
            var propertyValue = default(T);
            if (obj == null) return propertyValue;

            var propInfo = obj.GetType().GetProperty(propertyName,
                DefaultScopeBindingFlags);
            if (propInfo == null) return propertyValue;

            propertyValue = (T)propInfo.GetValue(obj);
            return propertyValue;
        }

        /// <summary>
        /// try to gives the value of a member of an object, first a field and secondly a property, whether it is public,protected or private
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <param name="memberName"></param>
        /// <param name="memberValue"></param>
        /// <returns>value or null</returns>
        public static bool GetMember<T>(this object obj, string memberName, out T memberValue)
            => obj.HasField(memberName) ?
                    GetField(obj, memberName, out memberValue)
                    : GetProperty(obj, memberName, out memberValue);

        /// <summary>
        /// try to gives the value of a member of an object, first a field and secondly a property, whether it is public,protected or private
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <param name="memberName"></param>
        /// <returns>value or null</returns>
        public static T GetMember<T>(this object obj, string memberName)
            => obj.HasField(memberName) ?
                    GetField<T>(obj, memberName)
                    : GetProperty<T>(obj, memberName);

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
