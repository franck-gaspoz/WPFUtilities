using System;
using System.Windows;
using System.Windows.Controls;

using WPFUtilities.Helpers;

namespace WPFUtilities.AttachedProperties.Scrolling
{
    /// <summary>
    /// auto scroll down
    /// </summary>
    public static class ScrollDownProperty
    {
        /// <summary>
        /// get is auto
        /// </summary>
        /// <param name="dependencyObject">dependency object</param>
        /// <returns>value</returns>
        public static bool GetIsAuto(DependencyObject dependencyObject)
        {
            return (bool)dependencyObject.GetValue(IsAutoProperty);
        }

        /// <summary>
        /// set is auto
        /// </summary>
        /// <param name="dependencyObject">dependency object</param>
        /// <param name="value">value</param>
        public static void SetIsAuto(DependencyObject dependencyObject, bool value)
        {
            dependencyObject.SetValue(IsAutoProperty, value);
        }

        /// <summary>
        /// is auto property
        /// </summary>
        public static readonly DependencyProperty IsAutoProperty =
            DependencyProperty.RegisterAttached(
                "IsAuto",
                typeof(bool),
                typeof(ScrollDownProperty),
                new PropertyMetadata(false, IsAutoChanged));

        static void IsAutoChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs eventArgs)
        {
            if (dependencyObject is FrameworkElement element)
            {
                void EnableAutoScrollDown(object src, EventArgs e)
                {
                    element.Loaded -= EnableAutoScrollDown;
                    var scrollViewer = WPFHelper.FindVisualChild<ScrollViewer>(element);
                    scrollViewer.ScrollChanged += (o, ea) =>
                    {
                        scrollViewer.ScrollToBottom();
                    };
                }
                element.Loaded += EnableAutoScrollDown;
            }
        }
    }
}
