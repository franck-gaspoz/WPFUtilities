using System.Windows;

namespace WPFUtilities.Components.Services.Properties
{
    public static partial class Command
    {
        #region Param4 dependency property

        /// <summary>
        /// command parameter 4 dependency property getter
        /// </summary>
        public static object GetParam4(DependencyObject dependencyObject)
            => (object)dependencyObject.GetValue(Param4Property);

        /// <summary>
        /// command parameter 4 dependency property setter
        /// </summary>
        public static void SetParam4(DependencyObject dependencyObject, object value)
            => dependencyObject.SetValue(Param4Property, value);

        /// <summary>
        /// command parameter 4 dependency property
        /// </summary>
        public static readonly DependencyProperty Param4Property =
                DependencyProperty.RegisterAttached(
                    "Param4",
                    typeof(object),
                    typeof(Command),
                    new PropertyMetadata(UnsetPropertyValue));

        #endregion

    }
}
