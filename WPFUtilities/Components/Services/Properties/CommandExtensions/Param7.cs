using System.Windows;

using WPFUtilities.Extensions.DependencyObjects;

namespace WPFUtilities.Components.Services.Properties
{
    public static partial class Command
    {
        #region Param7 dependency property

        /// <summary>
        /// command parameter 7 dependency property getter
        /// </summary>
        public static object GetParam7(DependencyObject dependencyObject)
            => (object)dependencyObject.GetValue(Param7Property);

        /// <summary>
        /// command parameter 7 dependency property setter
        /// </summary>
        public static void SetParam7(DependencyObject dependencyObject, object value)
            => dependencyObject.SetValue(Param7Property, value);

        /// <summary>
        /// command parameter 7 dependency property
        /// </summary>
        public static readonly DependencyProperty Param7Property =
            DependencyObjectExtensions.RegisterAttached(
                "Param7",
                typeof(object),
                typeof(Command),
                new PropertyMetadata((object)UnsetPropertyValue));

        #endregion

    }
}
