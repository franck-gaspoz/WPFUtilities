using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;

using WPFUtilities.Extensions.DependencyObjects;

namespace WPFUtilities.Components.UI
{
    /// <summary>
    /// set child spacing
    /// </summary>
    public static partial class Layout
    {
        /// <summary>
        /// get spacing
        /// </summary>
        /// <param name="dependencyObject">dependency object</param>
        /// <returns>value</returns>
        public static double GetChildSpacing(DependencyObject dependencyObject) => (double)dependencyObject.GetValue(ChildSpacingProperty);

        /// <summary>
        /// set spacing
        /// </summary>
        /// <param name="dependencyObject">dependency Object</param>
        /// <param name="ChildSpacing">ChildSpacing</param>
        public static void SetChildSpacing(DependencyObject dependencyObject, double ChildSpacing) => dependencyObject.SetValue(ChildSpacingProperty, ChildSpacing);

        /// <summary>
        /// margin property
        /// </summary>
        public static readonly DependencyProperty ChildSpacingProperty =
            DependencyObjectExtensions.RegisterAttached(
                "ChildSpacing",
                typeof(double),
                typeof(Layout),
                new UIPropertyMetadata(0d, ChildSpacingChanged));

        static void ChildSpacingChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs eventArgs)
        {
            if (DesignerProperties.GetIsInDesignMode(dependencyObject)) return;
            if (!(dependencyObject is Panel panel)) return;

            if (!panel.IsLoaded)
                panel.Loaded += Panel_Loaded_SetChildrensSpacing;
            else
                SetChildrensSpacing(panel);
        }

        private static void Panel_Loaded_SetChildrensSpacing(object sender, RoutedEventArgs e)
        {
            if (!(sender is Panel panel)) return;

            panel.Loaded -= Panel_Loaded_SetChildrensMargin;
            SetChildrensSpacing(panel);
        }

        static void SetChildrensSpacing(Panel panel)
        {
            var childSpacing = (double)panel.GetValue(ChildSpacingProperty);
            var count = panel.Children.Count;

            FrameworkElement GetChildAtIndex(int i) => panel.Children[i] as FrameworkElement;

            if (count == 1)
            {
                GetChildAtIndex(0).Margin = new Thickness(childSpacing, 0, childSpacing, 0);
            }
            int index = 0;
            foreach (var child in panel.Children)
            {
                GetChildAtIndex(index++).Margin = new Thickness(0, 0, childSpacing, 0);
            }
        }
    }
}
