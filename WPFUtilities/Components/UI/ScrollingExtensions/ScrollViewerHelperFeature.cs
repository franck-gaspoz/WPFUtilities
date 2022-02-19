﻿using System;
using System.Windows;
using System.Windows.Controls;

using WPFUtilities.Components.UI.ScrollingExtensions;

namespace WPFUtilities.Components.UI
{
    /// <summary>
    /// scroll viewer helper feature behavior. exports scroll viewer properties to a model
    /// </summary>
    public partial class Scrolling
    {
        #region scroll viewer helper feature

        /// <summary>
        /// get scroll viewer helper feature model
        /// </summary>
        /// <param name="dependencyObject">dependency Object</param>
        /// <returns></returns>
        public static IScrollViewerHelperViewProperties GetScrollViewerHelperViewPropertiesProperty(DependencyObject dependencyObject)
            => (IScrollViewerHelperViewProperties)dependencyObject.GetValue(ScrollViewerHelperViewPropertiesProperty);

        /// <summary>
        /// set scroll viewer helper feature model
        /// </summary>
        /// <param name="dependencyObject">dependency Object</param>
        /// <param name="value">value</param>
        public static void SetScrollViewerHelperViewPropertiesProperty(DependencyObject dependencyObject, IScrollViewerHelperViewProperties value)
            => dependencyObject.SetValue(ScrollViewerHelperViewPropertiesProperty, value);

        /// <summary>
        /// scroll viewer helper feature model
        /// </summary>
        public static readonly DependencyProperty ScrollViewerHelperViewPropertiesProperty =
            DependencyProperty.Register(
                "ScrollViewerHelperViewProperties",
                typeof(IScrollViewerHelperViewProperties),
                typeof(ScrollViewer),
                new PropertyMetadata(null));

        #endregion

        #region scroll viewer helper feature

        /// <summary>
        /// get scroll viewer helper feature model
        /// </summary>
        /// <param name="dependencyObject">dependency Object</param>
        /// <returns></returns>
        public static bool GetScrollViewerHelperIsEnabledProperty(DependencyObject dependencyObject)
            => (bool)dependencyObject.GetValue(ScrollViewerHelperIsEnabledProperty);

        /// <summary>
        /// set scroll viewer helper feature model
        /// </summary>
        /// <param name="dependencyObject">dependency Object</param>
        /// <param name="value">value</param>
        public static void SetScrollViewerHelperIsEnabledProperty(DependencyObject dependencyObject, bool value)
            => dependencyObject.SetValue(ScrollViewerHelperIsEnabledProperty, value);

        /// <summary>
        /// scroll viewer helper feature model
        /// </summary>
        public static readonly DependencyProperty ScrollViewerHelperIsEnabledProperty =
            DependencyProperty.Register(
                "ScrollViewerHelperIsEnabled",
                typeof(bool),
                typeof(ScrollViewer),
                new PropertyMetadata(ScrollViewerHelperIsEnabledChanged));

        #endregion

        static T GetResolveCreateService<T>(
            DependencyObject dependencyObject,
            DependencyProperty dependencyProperty,
            Func<T> create)
        {
            var service = (T)dependencyObject.GetValue(dependencyProperty);
            if (service != null) return service;
            //dependencyObject.WithService()
            return default(T);
        }

        static void ScrollViewerHelperIsEnabledChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs eventArgs)
        {
            if (!(dependencyObject is ScrollViewer scrollViewer)) return;

            if ((bool)eventArgs.NewValue)
                scrollViewer.ScrollChanged += ScrollViewer_ScrollChanged_HelperFeature;
            else
                scrollViewer.ScrollChanged -= ScrollViewer_ScrollChanged_HelperFeature;
        }

        /// <summary>
        /// scroll viewer scroll changed event handler
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="eventArgs">event args</param>
        static void ScrollViewer_ScrollChanged_HelperFeature(object sender, ScrollChangedEventArgs eventArgs)
        {
            if (!(sender is ScrollViewer scrollViewer)) return;

            var scrollViewerViewProperties = (IScrollViewerHelperViewProperties)scrollViewer.GetValue(ScrollViewerHelperViewPropertiesProperty);


            if (scrollViewerViewProperties == null) return;

            scrollViewerViewProperties.ScrollViewer = scrollViewer;
            if (eventArgs.HorizontalChange != 0)
                scrollViewerViewProperties.HorizontalOffset = eventArgs.HorizontalOffset;
            if (eventArgs.VerticalChange != 0)
                scrollViewerViewProperties.VerticalOffset = eventArgs.VerticalOffset;
        }
    }
}