using System.Windows;

using WPFUtilities.Extensions.DependencyObjects;

namespace WPFUtilities.Components.Services.Properties
{
    public static partial class Command
    {
        #region Param9 dependency property

        /// <summary>
        /// command parameter 9 dependency property getter
        /// </summary>
        public static object GetParam9(DependencyObject dependencyObject)
            => (object)dependencyObject.GetValue(Param9Property);

        /// <summary>
        /// command parameter 9 dependency property setter
        /// </summary>
        public static void SetParam9(DependencyObject dependencyObject, object value)
            => dependencyObject.SetValue(Param9Property, value);

        /// <summary>
        /// command parameter 9 dependency property
        /// </summary>
        public static readonly DependencyProperty Param9Property =
            DependencyObjectExtensions.RegisterAttached(
                "Param9",
                typeof(object),
                typeof(Command),
                new PropertyMetadata((object)UnsetPropertyValue));

        #endregion

    }
}
