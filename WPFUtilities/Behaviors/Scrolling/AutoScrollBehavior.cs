using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;

using Microsoft.Xaml.Behaviors;

using WPFUtilities.Helpers;

namespace WPFUtilities.Behaviors.Scrolling
{
    /// <summary>
    /// auto scroll at bottom behavior
    /// </summary>
    public class AutoScrollBehavior :
        Behavior<FrameworkElement>
    {
        ScrollViewer _scrollViewer;

        #region BindingList

        /// <summary>
        /// scroll viewer items source 
        /// </summary>
        public IBindingList BindingList
        {
            get { return (IBindingList)GetValue(BindingListProperty); }
            set { SetValue(BindingListProperty, value); }
        }

        /// <summary>
        /// set binding list
        /// </summary>
        /// <param name="dependencObject">dependency object</param>
        /// <returns></returns>
        public static IBindingList GetBindingListProperty(DependencyObject dependencObject)
            => (IBindingList)dependencObject.GetValue(BindingListProperty);

        /// <summary>
        /// set binding list
        /// </summary>
        /// <param name="dependencyObject">dependency object</param>
        /// <param name="value">value</param>
        public static void SetBindingListProperty(DependencyObject dependencyObject, IBindingList value)
            => dependencyObject.SetValue(BindingListProperty, value);

        /// <summary>
        /// binding list dependency property
        /// </summary>
        public static readonly DependencyProperty BindingListProperty =
            DependencyProperty.Register("BindingList", typeof(IBindingList), typeof(AutoScrollBehavior), new PropertyMetadata(null));

        #endregion

        /// <inheritdoc/>
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
            => _scrollViewer?.ScrollToBottom();

        /// <inheritdoc/>
        protected override void OnDetaching()
        {
            if (IsInitiliazed)
                BindingList.ListChanged -= BindingList_ListChanged;
        }
    }
}
