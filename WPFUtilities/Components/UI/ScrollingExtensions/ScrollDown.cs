using System;
using System.Collections.Concurrent;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;

using WPFUtilities.Extensions.DependencyObjects;
using WPFUtilities.Helpers;

namespace WPFUtilities.Components.UI
{
    /// <summary>
    /// auto scroll down
    /// </summary>
    public static partial class Scrolling
    {
        #region is auto

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
            DependencyObjectExtensions.RegisterAttached(
                "IsAuto",
                typeof(bool),
                typeof(Scrolling),
                new PropertyMetadata(false, IsAutoChanged));

        #endregion

        static Lazy<ConcurrentDictionary<object, ScrollViewer>> _scrollViewers = new Lazy<ConcurrentDictionary<object, ScrollViewer>>();

        static void IsAutoChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs eventArgs)
        {
            if (DesignerProperties.GetIsInDesignMode(dependencyObject)) return;
            if (!(dependencyObject is ListBox element)) return;

            if ((bool)eventArgs.NewValue)
            {
                if (!element.IsLoaded)
                    element.Loaded += ListBox_Loaded_EnableAutoScrollDown;
                else
                {
                    _ = _scrollViewers.Value.GetOrAdd(
                        element.ItemContainerGenerator,
                        WPFHelper.FindVisualChild<ScrollViewer>(element));

                    element.ItemContainerGenerator.ItemsChanged += ItemContainerGenerator_ItemsChanged;
                }
            }
            else
                element.ItemContainerGenerator.ItemsChanged -= ItemContainerGenerator_ItemsChanged;
        }

        static void ListBox_Loaded_EnableAutoScrollDown(object src, EventArgs e)
        {
            if (!(src is ListBox element)) return;

            element.Loaded -= ListBox_Loaded_EnableAutoScrollDown;
            var scrollViewer = _scrollViewers.Value.GetOrAdd(
                element.ItemContainerGenerator,
                WPFHelper.FindVisualChild<ScrollViewer>(element));

            element.ItemContainerGenerator.ItemsChanged += ItemContainerGenerator_ItemsChanged;
        }

        private static void ItemContainerGenerator_ItemsChanged(object sender, ItemsChangedEventArgs e)
        {
            if (_scrollViewers.Value.TryGetValue(sender, out var scrollViewer))
                scrollViewer.ScrollToBottom();
        }
    }
}
