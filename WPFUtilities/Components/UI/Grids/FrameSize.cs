using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace WPFUtilities.Components.UI
{
    /// <summary>
    /// set frame size
    /// </summary>
    public static partial class Grids
    {
        #region size

        /// <summary>
        /// get size
        /// </summary>
        /// <param name="dependencyObject">dependency object</param>
        /// <returns>value</returns>
        public static double GetFrameSize(DependencyObject dependencyObject) => (double)dependencyObject.GetValue(FrameSizeProperty);

        /// <summary>
        /// set margin
        /// </summary>
        /// <param name="dependencyObject">dependency Object</param>
        /// <param name="FrameSize">FrameSize</param>
        public static void SetFrameSize(DependencyObject dependencyObject, double FrameSize) => dependencyObject.SetValue(FrameSizeProperty, FrameSize);

        /// <summary>
        /// margin property
        /// </summary>
        public static readonly DependencyProperty FrameSizeProperty =
            DependencyProperty.RegisterAttached(
                "FrameSize",
                typeof(double),
                typeof(Grids),
                new UIPropertyMetadata(1d, FrameSizeChanged));

        #endregion

        static void FrameSizeChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs eventArgs)
        {
            if (DesignerProperties.GetIsInDesignMode(dependencyObject)) return;
            if (!(dependencyObject is Grid panel)) return;

            if (!panel.IsLoaded)
                panel.Loaded += Panel_Loaded_SetChildrensFrameSize;
            else
                SetChildrensFrame(panel);
        }

        private static void Panel_Loaded_SetChildrensFrameSize(object sender, RoutedEventArgs e)
        {
            if (!(sender is Grid panel)) return;

            panel.Loaded -= Panel_Loaded_SetChildrensFrameSize;
            SetChildrensFrame(panel);
        }

        static void SetChildrensFrame(Grid panel)
        {
            var size = (double)panel.GetValue(FrameSizeProperty);
            var brush = (Brush)panel.GetValue(FrameBrushProperty);
            var xmax = panel.ColumnDefinitions.Count - 1;
            var ymax = panel.RowDefinitions.Count - 1;
            foreach (var item in panel.Children)
            {
                if (item is FrameworkElement elem)
                {
                    var x = Grid.GetColumn(elem);
                    var y = Grid.GetRow(elem);
                    var thLeft = size;
                    var thTop = size;
                    var thRight = x == xmax ? size : 0;
                    var thBottom = y == ymax ? size : 0;
                    var thickness = new Thickness(thLeft, thTop, thRight, thBottom);
                    elem.Margin = thickness;
                }
            }
            panel.Margin = new Thickness(0d);
            panel.Background = brush;
        }
    }
}
