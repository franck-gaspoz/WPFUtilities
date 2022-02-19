using System;
using System.Collections.Concurrent;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;

using WPFUtilities.Helpers;

namespace WPFUtilities.Components.UI
{
    /// <summary>
    /// auto scroll down
    /// </summary>
    public static partial class Scrolling
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
                typeof(Scrolling),
                new PropertyMetadata(false, IsAutoChanged));

        static Lazy<ConcurrentDictionary<object, ScrollViewer>> _scrollViewers = new Lazy<ConcurrentDictionary<object, ScrollViewer>>();

        static void IsAutoChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs eventArgs)
        {
            if (dependencyObject is ListBox element)
            {
                void EnableAutoScrollDown(object src, EventArgs e)
                {
                    element.Loaded -= EnableAutoScrollDown;
                    var scrollViewer = _scrollViewers.Value.GetOrAdd(element.ItemContainerGenerator, WPFHelper.FindVisualChild<ScrollViewer>(element));
                    element.ItemContainerGenerator.ItemsChanged += ItemContainerGenerator_ItemsChanged;
                }
                if (!_scrollViewers.Value.ContainsKey(element))
                    element.Loaded += EnableAutoScrollDown;
                else
                {
                    if ((bool)eventArgs.NewValue)
                        element.ItemContainerGenerator.ItemsChanged += ItemContainerGenerator_ItemsChanged;
                    else
                        element.ItemContainerGenerator.ItemsChanged -= ItemContainerGenerator_ItemsChanged;
                }
            }
        }

        private static void ItemContainerGenerator_ItemsChanged(object sender, ItemsChangedEventArgs e)
        {
            if (_scrollViewers.Value.TryGetValue(sender, out var scrollViewer))
                scrollViewer.ScrollToBottom();
        }
    }
}
