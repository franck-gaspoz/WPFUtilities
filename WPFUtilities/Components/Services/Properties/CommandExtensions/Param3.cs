using System.Windows;

namespace WPFUtilities.Components.Services.Properties
{
    public static partial class Command
    {
        #region Param3 dependency property

        /// <summary>
        /// command parameter 3 dependency property getter
        /// </summary>
        public static object GetParam3(DependencyObject dependencyObject)
            => (object)dependencyObject.GetValue(Param3Property);

        /// <summary>
        /// command parameter 3 dependency property setter
        /// </summary>
        public static void SetParam3(DependencyObject dependencyObject, object value)
            => dependencyObject.SetValue(Param3Property, value);

        /// <summary>
        /// command parameter 3 dependency property
        /// </summary>
        public static readonly DependencyProperty Param3Property =
                DependencyProperty.RegisterAttached(
                    "Param3",
                    typeof(object),
                    typeof(Command),
                    new PropertyMetadata(UnsetPropertyValue));

        #endregion

    }
}
