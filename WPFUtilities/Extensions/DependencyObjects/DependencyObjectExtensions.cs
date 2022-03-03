using System.Windows;

namespace WPFUtilities.Extensions.DependencyObjects
{
    /// <summary>
    /// dependency objects extensions
    /// </summary>
    public static class DependencyObjectExtensions
    {
        /// <summary>
        /// add a new or get an existing dependency property value
        /// </summary>
        /// <typeparam name="T">expected value type of the property</typeparam>
        /// <param name="dependencyObject">dependency object</param>
        /// <param name="property">property</param>
        /// <returns>value of T</returns>
        public static T AddOrGetValue<T>(
            this DependencyObject dependencyObject,
            DependencyProperty property)
            where T : new()
        {
            var value = dependencyObject.GetValue<T>(property);
            if (value == null)
            {
                value = new T();
                dependencyObject.SetValue<T>(property, value);
            }
            return value;
        }

        /// <summary>
        /// sets the value of a dependency project from a value of a given type
        /// </summary>
        /// <typeparam name="T">value type</typeparam>
        /// <param name="dependencyObject">dependency object</param>
        /// <param name="property">property</param>
        /// <param name="value">value</param>
        public static void SetValue<T>(
            this DependencyObject dependencyObject,
            DependencyProperty property,
            T value)
            => dependencyObject.SetValue(property, value);

        /// <summary>
        /// returns value of dependency property casted to T
        /// </summary>
        /// <typeparam name="T">expected value type</typeparam>
        /// <param name="dependencyObject">dependency object</param>
        /// <param name="property">property</param>
        /// <returns>property value of T</returns>
        public static T GetValue<T>(
            this DependencyObject dependencyObject,
            DependencyProperty property
            )
            => (T)dependencyObject.GetValue(property);
    }
}
