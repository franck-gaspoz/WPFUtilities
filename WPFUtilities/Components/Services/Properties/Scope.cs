using System.Windows;

using WPFUtilities.Extensions.DependencyObjects;

namespace WPFUtilities.Components.Services.Properties
{
    /// <summary>
    /// set Value for service lookup: global or local (default)
    /// <para>this property is used by service lookup operations</para>
    /// </summary>
    public class Scope
    {
        #region Value

        /// <summary>
        /// get Value dependency property value for object
        /// </summary>
        /// <param name="dependencyObject">dependency object</param>
        /// <returns>true if is enabled</returns>
        public static Scopes GetValue(DependencyObject dependencyObject)
            => (Scopes)dependencyObject.GetValue(ValueProperty);

        /// <summary>
        /// set Value dependency property value for object
        /// </summary>
        /// <param name="dependencyObject">dependency object</param>
        /// <param name="value">value</param>
        public static void SetValue(DependencyObject dependencyObject, Scopes value)
            => dependencyObject.SetValue(ValueProperty, value);

        /// <summary>
        /// Value dependency property
        /// </summary>
        public static readonly DependencyProperty ValueProperty =
            DependencyObjectExtensions.RegisterAttached(
                "Value",
                typeof(Scopes),
                typeof(Scope),
                new PropertyMetadata(Scopes.Local));

        #endregion
    }
}
