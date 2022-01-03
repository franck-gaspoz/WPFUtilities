using System.Windows;
using System.Windows.Controls;

using Microsoft.Xaml.Behaviors;

namespace WPFUtilities.Behaviors.ScrollViewers
{
    public class ScrollViewerHelperFeatureBehavior :
        Behavior<ScrollViewer>
    {
        public IScrollViewerHelperFeature ScrollViewerHelperFeature
        {
            get { return (IScrollViewerHelperFeature)GetValue(ScrollViewerHelperFeatureProperty); }
            set { SetValue(ScrollViewerHelperFeatureProperty, value); }
        }

        public static IScrollViewerHelperFeature GetScrollViewerHelperFeatureProperty(DependencyObject obj)
        {
            return (IScrollViewerHelperFeature)obj.GetValue(ScrollViewerHelperFeatureProperty);
        }

        public static void SetScrollViewerHelperFeatureProperty(DependencyObject obj, IScrollViewerHelperFeature value)
        {
            obj.SetValue(ScrollViewerHelperFeatureProperty, value);
        }

        public static readonly DependencyProperty ScrollViewerHelperFeatureProperty =
            DependencyProperty.Register("ScrollViewerHelperFeature", typeof(IScrollViewerHelperFeature), typeof(ScrollViewerHelperFeatureBehavior), new PropertyMetadata(null));


        protected override void OnAttached()
        {
            ScrollViewerHelperFeature.ScrollViewer = AssociatedObject;
            AssociatedObject.ScrollChanged += AssociatedObject_ScrollChanged;
            //AssociatedObject.SizeChanged += AssociatedObject_SizeChanged;
        }

        //static int _count = 0;
        private void AssociatedObject_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            //Log.Debug($"ScrollViewer size changed ({_count++})");
        }

        private void AssociatedObject_ScrollChanged(object sender, ScrollChangedEventArgs e)
        {
            var m = ScrollViewerHelperFeature;
            if (e.HorizontalChange != 0)
                m.HorizontalOffset = e.HorizontalOffset;
            if (e.VerticalChange != 0)
                m.VerticalOffset = e.VerticalOffset;
        }

        protected override void OnDetaching()
        {
            AssociatedObject.ScrollChanged -= AssociatedObject_ScrollChanged;
            ScrollViewerHelperFeature.ScrollViewer = null;
        }
    }
}
