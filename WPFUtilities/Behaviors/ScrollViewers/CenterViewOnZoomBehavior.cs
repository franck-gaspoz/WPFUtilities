using System.Windows;
using System.Windows.Controls;

using GIS.Common.Logger;

using Microsoft.Xaml.Behaviors;

namespace WPFUtilities.Behaviors.ScrollViewers
{
    public class CenterZoomOnViewBehavior :
        Behavior<FrameworkElement>
    {
        #region ScrollViewer

        public ScrollViewer ScrollViewer
        {
            get { return (ScrollViewer)GetValue(ScrollViewerProperty); }
            set { SetValue(ScrollViewerProperty, value); }
        }

        public static ScrollViewer GetScrollViewerProperty(DependencyObject obj)
        {
            return (ScrollViewer)obj.GetValue(ScrollViewerProperty);
        }

        public static void SetScrollViewerProperty(DependencyObject obj, ScrollViewer value)
        {
            obj.SetValue(ScrollViewerProperty, value);
        }

        public static readonly DependencyProperty ScrollViewerProperty =
            DependencyProperty.Register("ScrollViewer", typeof(ScrollViewer), typeof(CenterZoomOnViewBehavior), new PropertyMetadata(null));

        #endregion

        protected override void OnAttached()
        {
            AssociatedObject.SizeChanged += AssociatedObject_SizeChanged; ;
        }

        private void AssociatedObject_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            Log.Debug($"map layers: size changed ({e.PreviousSize.Width},{e.PreviousSize.Height}) -> ({e.NewSize.Width},{e.NewSize.Height})");
        }

        /*bool IsInitiliazed = false;

        private void AssociatedObject_Loaded(object sender, RoutedEventArgs e)
        {
            if (!IsInitiliazed)
            {
                IsInitiliazed = true;
                AssociatedObject.Loaded -= AssociatedObject_Loaded;
            }
        }*/

        protected override void OnDetaching()
        {
            AssociatedObject.SizeChanged -= AssociatedObject_SizeChanged;
        }
    }
}
