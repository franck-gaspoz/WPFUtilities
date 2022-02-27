using System.Windows;

namespace WPFUtilities.Components.Services.Properties
{
    public static partial class Command
    {
        #region Param5 dependency property

        /// <summary>
        /// command parameter 5 dependency property getter
        /// </summary>
        public static object GetParam5(DependencyObject dependencyObject)
            => (object)dependencyObject.GetValue(Param5Property);

        /// <summary>
        /// command parameter 5 dependency property setter
        /// </summary>
        public static void SetParam5(DependencyObject dependencyObject, object value)
            => dependencyObject.SetValue(Param5Property, value);

        /// <summary>
        /// command parameter 5 dependency property
        /// </summary>
        public static readonly DependencyProperty Param5Property =
                DependencyProperty.RegisterAttached(
                    "Param5",
                    typeof(object),
                    typeof(Command),
                    new PropertyMetadata((object)UnsetPropertyValue));

        #endregion

    }
}
