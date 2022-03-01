using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace WPFUtilities.Components.UI
{
    /// <summary>
    /// set frame brush
    /// </summary>
    public static partial class Grids
    {
        #region brush

        /// <summary>
        /// get brush
        /// </summary>
        /// <param name="dependencyObject">dependency object</param>
        /// <returns>value</returns>
        public static Brush GetFrameBrush(DependencyObject dependencyObject) => (Brush)dependencyObject.GetValue(FrameBrushProperty);

        /// <summary>
        /// set brush
        /// </summary>
        /// <param name="dependencyObject">dependency Object</param>
        /// <param name="FrameBrush">FrameBrush</param>
        public static void SetFrameBrush(DependencyObject dependencyObject, Brush FrameBrush) => dependencyObject.SetValue(FrameBrushProperty, FrameBrush);

        /// <summary>
        /// margin property
        /// </summary>
        public static readonly DependencyProperty FrameBrushProperty =
            DependencyProperty.RegisterAttached(
                "FrameBrush",
                typeof(Brush),
                typeof(Grids),
                new UIPropertyMetadata(null, FrameBrushChanged));

        #endregion

        static void FrameBrushChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs eventArgs)
        {
            if (DesignerProperties.GetIsInDesignMode(dependencyObject)) return;
            if (!(dependencyObject is Grid panel)) return;

            if (!panel.IsLoaded)
                panel.Loaded += Panel_Loaded_SetChildrensFrameBrush;
            else
                SetChildrensFrame(panel);
        }

        private static void Panel_Loaded_SetChildrensFrameBrush(object sender, RoutedEventArgs e)
        {
            if (!(sender is Grid panel)) return;

            panel.Loaded -= Panel_Loaded_SetChildrensFrameBrush;
            SetChildrensFrame(panel);
        }
    }
}
