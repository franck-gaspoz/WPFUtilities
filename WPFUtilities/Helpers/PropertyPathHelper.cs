using System.Windows;
using System.Windows.Data;

using WPFUtilities.Extensions.DependencyObjects;

namespace WPFUtilities.Helpers
{
    /// <summary>
    /// wpf property path helper
    /// </summary>
    public static class PropertyPathHelper
    {
        /// <summary>
        /// get the value from a binding property path
        /// </summary>
        /// <param name="obj">source object</param>
        /// <param name="propertyPath">property path</param>
        /// <returns>source data</returns>
        public static object GetValue(object obj, string propertyPath)
        {
            Binding binding = new Binding(propertyPath);
            binding.Mode = BindingMode.OneTime;
            binding.Source = obj;
            BindingOperations.SetBinding(_dummy, Dummy.ValueProperty, binding);
            var result = _dummy.GetValue(Dummy.ValueProperty);
            BindingOperations.ClearBinding(_dummy, Dummy.ValueProperty);
            return result;
        }

        private static readonly Dummy _dummy = new Dummy();

        private class Dummy : DependencyObject
        {
            public static readonly DependencyProperty ValueProperty =
                DependencyObjectExtensions.Register(
                    "Value",
                    typeof(object),
                    typeof(Dummy),
                    new UIPropertyMetadata(null));
        }
    }
}
