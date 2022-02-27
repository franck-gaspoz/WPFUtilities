using System.Windows;

namespace WPFUtilities.Components.Services.Properties
{
    public static partial class Command
    {
        /// <summary>
        /// value of an unset Param.. Property
        /// </summary>
        public const string UnsetPropertyValue = "UnsetPropertyValue";

        #region Param2 dependency property

        /// <summary>
        /// command parameter 2 dependency property getter
        /// </summary>
        public static object GetParam2(DependencyObject dependencyObject)
            => (object)dependencyObject.GetValue(Param2Property);

        /// <summary>
        /// command parameter 2 dependency property setter
        /// </summary>
        public static void SetParam2(DependencyObject dependencyObject, object value)
            => dependencyObject.SetValue(Param2Property, value);

        /// <summary>
        /// command parameter 2 dependency property
        /// </summary>
        public static readonly DependencyProperty Param2Property =
                DependencyProperty.RegisterAttached(
                    "Param2",
                    typeof(object),
                    typeof(Command),
                    new PropertyMetadata((object)UnsetPropertyValue));

        #endregion

    }
}
