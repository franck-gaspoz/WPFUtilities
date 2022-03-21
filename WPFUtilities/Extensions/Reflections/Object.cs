using System;
using System.Collections.Generic;
using System.Linq;

namespace WPFUtilities.Extensions.Reflections
{
    /// <summary>
    /// object extensions
    /// </summary>
    public static class Object
    {
        /// <summary>
        /// creates a shallow clone of source object
        /// <para>copy public properties settable values</para>
        /// </summary>
        /// <typeparam name="T">object type</typeparam>
        /// <param name="source">source to be shallowed clone</param>
        /// <returns>object of type T with gettable/settable public properties initialized or default(T) if source is null</returns>
        public static T ShallowClone<T>(this object source)
        {
            if (source == null) return default(T);
            var target = (T)Activator.CreateInstance(typeof(T));
            foreach (var prop in typeof(T).GetProperties())
                if (prop.CanRead && prop.CanWrite)
                    prop.SetValue(target,
                        prop.GetValue(source));
            return target;
        }

        /// <summary>
        /// get dump of properties of an object
        /// </summary>
        /// <param name="o">object</param>
        /// <param name="selectedProperties">only these selected properties if not null and any</param>
        /// <param name="separator">properties dumps separator</param>
        /// <returns>multiline string</returns>
        public static string GetPropertiesDump(
            this object o,
            IEnumerable<string> selectedProperties = null,
            string separator = ","
            )
        {
            if (o == null) return string.Empty;
            var sb = new List<string>();
            foreach (var prop in o.GetType().GetProperties()
                .Where(prop => prop.CanRead)
                )
                if (selectedProperties == null
                    || !selectedProperties.Any()
                    || selectedProperties.Contains(prop.Name))
                    sb.Add(prop.Name + "=" + prop.GetValue(o));
            return string.Join(separator, sb);
        }
    }
}
