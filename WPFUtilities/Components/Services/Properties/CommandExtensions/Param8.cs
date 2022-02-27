using System.Windows;

namespace WPFUtilities.Components.Services.Properties
{
    public static partial class Command
    {
        #region Param8 dependency property

        /// <summary>
        /// command parameter 8 dependency property getter
        /// </summary>
        public static object GetParam8(DependencyObject dependencyObject)
            => (object)dependencyObject.GetValue(Param8Property);

        /// <summary>
        /// command parameter 8 dependency property setter
        /// </summary>
        public static void SetParam8(DependencyObject dependencyObject, object value)
            => dependencyObject.SetValue(Param8Property, value);

        /// <summary>
        /// command parameter 8 dependency property
        /// </summary>
        public static readonly DependencyProperty Param8Property =
                DependencyProperty.RegisterAttached(
                    "Param8",
                    typeof(object),
                    typeof(Command),
                    new PropertyMetadata((object)UnsetPropertyValue));

        #endregion

    }
}
