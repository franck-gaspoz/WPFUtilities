using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace WPFUtilities.Components.UI
{
    /// <summary>
    /// set childs margins
    /// </summary>
    public static partial class Grid
    {
        /// <summary>
        /// get margin
        /// </summary>
        /// <param name="dependencyObject">dependency object</param>
        /// <returns>value</returns>
        public static Brush GetFrameBrush(DependencyObject dependencyObject) => (Brush)dependencyObject.GetValue(FrameBrushProperty);

        /// <summary>
        /// set margin
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
                typeof(Grid),
                new UIPropertyMetadata(1d, FrameBrushChanged));

        static void FrameBrushChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs eventArgs)
        {
            if (DesignerProperties.GetIsInDesignMode(dependencyObject)) return;
            if (!(dependencyObject is Panel panel)) return;

            if (!panel.IsLoaded)
                panel.Loaded += Panel_Loaded_SetChildrensFrameBrush;
            else
                SetChildrensFrame(panel);
        }

        private static void Panel_Loaded_SetChildrensFrameBrush(object sender, RoutedEventArgs e)
        {
            if (!(sender is Panel panel)) return;

            panel.Loaded -= Panel_Loaded_SetChildrensFrameBrush;
            SetChildrensFrame(panel);
        }
    }
}
