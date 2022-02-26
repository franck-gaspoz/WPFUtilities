using System;

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
    }
}
