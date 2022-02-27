using System.Windows;

namespace WPFUtilities.Components.Services.Properties
{
    public static partial class Command
    {
        #region Param10 dependency property

        /// <summary>
        /// command parameter 10 dependency property getter
        /// </summary>
        public static object GetParam10(DependencyObject dependencyObject)
            => (object)dependencyObject.GetValue(Param10Property);

        /// <summary>
        /// command parameter 10 dependency property setter
        /// </summary>
        public static void SetParam10(DependencyObject dependencyObject, object value)
            => dependencyObject.SetValue(Param10Property, value);

        /// <summary>
        /// command parameter 10 dependency property
        /// </summary>
        public static readonly DependencyProperty Param10Property =
                DependencyProperty.RegisterAttached(
                    "Param10",
                    typeof(object),
                    typeof(Command),
                    new PropertyMetadata(UnsetPropertyValue));

        #endregion

    }
}
