using System.Windows;
using System.Windows.Controls;

using Microsoft.Xaml.Behaviors;

namespace WPFUtilities.Behaviors.Scrolling
{
    /// <summary>
    /// scroll viewer helper feature behavior. exports scroll viewer properties to a model
    /// </summary>
    public class ScrollViewerHelperFeatureBehavior :
        Behavior<ScrollViewer>
    {
        #region scroll viewer helper feature

        /// <summary>
        /// scroll viewer helper feature model
        /// </summary>
        public IScrollViewerHelperFeature ScrollViewerHelperFeature
        {
            get { return (IScrollViewerHelperFeature)GetValue(ScrollViewerHelperFeatureProperty); }
            set { SetValue(ScrollViewerHelperFeatureProperty, value); }
        }

        /// <summary>
        /// get scroll viewer helper feature model
        /// </summary>
        /// <param name="dependencyObject">dependency Object</param>
        /// <returns></returns>
        public static IScrollViewerHelperFeature GetScrollViewerHelperFeatureProperty(DependencyObject dependencyObject)
        {
            return (IScrollViewerHelperFeature)dependencyObject.GetValue(ScrollViewerHelperFeatureProperty);
        }

        /// <summary>
        /// set scroll viewer helper feature model
        /// </summary>
        /// <param name="dependencyObject">dependency Object</param>
        /// <param name="value">value</param>
        public static void SetScrollViewerHelperFeatureProperty(DependencyObject dependencyObject, IScrollViewerHelperFeature value)
        {
            dependencyObject.SetValue(ScrollViewerHelperFeatureProperty, value);
        }

        /// <summary>
        /// scroll viewer helper feature model
        /// </summary>
        public static readonly DependencyProperty ScrollViewerHelperFeatureProperty =
            DependencyProperty.Register("ScrollViewerHelperFeature", typeof(IScrollViewerHelperFeature), typeof(ScrollViewerHelperFeatureBehavior), new PropertyMetadata(null));

        #endregion

        /// <inheritdoc/>
        protected override void OnAttached()
        {
            ScrollViewerHelperFeature.ScrollViewer = AssociatedObject;
            AssociatedObject.ScrollChanged += AssociatedObject_ScrollChanged;
        }

        /// <summary>
        /// scroll viewer scroll changed event handler
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="eventArgs">event args</param>
        private void AssociatedObject_ScrollChanged(object sender, ScrollChangedEventArgs eventArgs)
        {
            var m = ScrollViewerHelperFeature;
            if (eventArgs.HorizontalChange != 0)
                m.HorizontalOffset = eventArgs.HorizontalOffset;
            if (eventArgs.VerticalChange != 0)
                m.VerticalOffset = eventArgs.VerticalOffset;
        }

        /// <inheritdoc/>
        protected override void OnDetaching()
        {
            AssociatedObject.ScrollChanged -= AssociatedObject_ScrollChanged;
            ScrollViewerHelperFeature.ScrollViewer = null;
        }
    }
}
