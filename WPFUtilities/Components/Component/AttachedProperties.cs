using System.Windows;

namespace WPFUtilities.Components.Component
{
    /// <summary>
    /// component host service dependency property
    /// </summary>
    public static class AttachedProperties
    {
        /// <summary>
        /// get component host
        /// </summary>
        /// <param name="obj">dependency object</param>
        /// <returns>component host</returns>
        public static IComponentHost GetComponentHost(DependencyObject obj)
        {
            return (IComponentHost)obj.GetValue(ComponentHostProperty);
        }

        /// <summary>
        /// set component host
        /// </summary>
        /// <param name="obj">dependency object</param>
        /// <param name="value">component host</param>
        public static void SetComponentHost(DependencyObject obj, IComponentHost value)
        {
            obj.SetValue(ComponentHostProperty, value);
        }

        /// <summary>
        /// component host property
        /// </summary>
        public static readonly DependencyProperty ComponentHostProperty =
            DependencyProperty.RegisterAttached(
                "ComponentHost",
                typeof(IComponentHost),
                typeof(AttachedProperties),
                new PropertyMetadata(null));
    }
}
