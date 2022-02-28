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
        public static double GetFrameWidth(DependencyObject dependencyObject) => (double)dependencyObject.GetValue(FrameWidthProperty);

        /// <summary>
        /// set margin
        /// </summary>
        /// <param name="dependencyObject">dependency Object</param>
        /// <param name="FrameWidth">FrameWidth</param>
        public static void SetFrameWidth(DependencyObject dependencyObject, double FrameWidth) => dependencyObject.SetValue(FrameWidthProperty, FrameWidth);

        /// <summary>
        /// margin property
        /// </summary>
        public static readonly DependencyProperty FrameWidthProperty =
            DependencyProperty.RegisterAttached(
                "FrameWidth",
                typeof(double),
                typeof(Grid),
                new UIPropertyMetadata(1d, FrameWidthChanged));

        static void FrameWidthChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs eventArgs)
        {
            if (DesignerProperties.GetIsInDesignMode(dependencyObject)) return;
            if (!(dependencyObject is Panel panel)) return;

            if (!panel.IsLoaded)
                panel.Loaded += Panel_Loaded_SetChildrensFrameWidth;
            else
                SetChildrensFrame(panel);
        }

        private static void Panel_Loaded_SetChildrensFrameWidth(object sender, RoutedEventArgs e)
        {
            if (!(sender is Panel panel)) return;

            panel.Loaded -= Panel_Loaded_SetChildrensFrameWidth;
            SetChildrensFrame(panel);
        }

        static void SetChildrensFrame(Panel panel)
        {
            var width = (double)panel.GetValue(FrameWidthProperty);
            var brush = (Brush)panel.GetValue(FrameBrushProperty);
            foreach (var child in panel.Children)
            {

            }
        }
    }
}
