using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;

using Microsoft.Xaml.Behaviors;

namespace WPFUtilities.Behaviors
{
    public class AutoScrollBehavior :
        Behavior<FrameworkElement>
    {
        ScrollViewer _scrollViewer;

        #region BindingList

        public IBindingList BindingList
        {
            get { return (IBindingList)GetValue(BindingListProperty); }
            set { SetValue(BindingListProperty, value); }
        }

        public static IBindingList GetBindingListProperty(DependencyObject obj)
        {
            return (IBindingList)obj.GetValue(BindingListProperty);
        }

        public static void SetBindingListProperty(DependencyObject obj, IBindingList value)
        {
            obj.SetValue(BindingListProperty, value);
        }

        public static readonly DependencyProperty BindingListProperty =
            DependencyProperty.Register("BindingList", typeof(IBindingList), typeof(AutoScrollBehavior), new PropertyMetadata(null));

        #endregion

        protected override void OnAttached()
        {
            AssociatedObject.Loaded += AssociatedObject_Loaded;
        }

        bool IsInitiliazed = false;

        private void AssociatedObject_Loaded(object sender, RoutedEventArgs e)
        {
            _scrollViewer = WPFHelper.FindVisualChild<ScrollViewer>(AssociatedObject);
            if (_scrollViewer != null && !IsInitiliazed)
            {
                AssociatedObject.Loaded -= AssociatedObject_Loaded;
                BindingList.ListChanged += BindingList_ListChanged;
                IsInitiliazed = true;
            }
        }

        private void BindingList_ListChanged(object sender, ListChangedEventArgs e)
        {
            _scrollViewer?.ScrollToBottom();
        }

        protected override void OnDetaching()
        {
            if (IsInitiliazed)
                BindingList.ListChanged -= BindingList_ListChanged;
        }
    }
}
