using System.Windows;

using WPFUtilities.Extensions.DependencyObjects;

namespace WPFUtilities.Components.Services.Properties
{
    public static partial class Command
    {
        #region Param6 dependency property

        /// <summary>
        /// command parameter 6 dependency property getter
        /// </summary>
        public static object GetParam6(DependencyObject dependencyObject)
            => (object)dependencyObject.GetValue(Param6Property);

        /// <summary>
        /// command parameter 6 dependency property setter
        /// </summary>
        public static void SetParam6(DependencyObject dependencyObject, object value)
            => dependencyObject.SetValue(Param6Property, value);

        /// <summary>
        /// command parameter 6 dependency property
        /// </summary>
        public static readonly DependencyProperty Param6Property =
            DependencyObjectExtensions.RegisterAttached(
                "Param6",
                typeof(object),
                typeof(Command),
                new PropertyMetadata((object)UnsetPropertyValue));

        #endregion

    }
}
