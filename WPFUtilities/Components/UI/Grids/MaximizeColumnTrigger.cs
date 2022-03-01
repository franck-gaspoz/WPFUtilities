﻿using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;

namespace WPFUtilities.Components.UI
{
    /// <summary>
    /// maximize column trigger
    /// </summary>
    public static partial class Grids
    {
        #region MaximizeColumnTriggerChanged

        /// <summary>
        /// get margin
        /// </summary>
        /// <param name="dependencyObject">dependency object</param>
        /// <returns>value</returns>
        public static bool? GetMaximizeColumnTrigger(DependencyObject dependencyObject) => (bool)dependencyObject.GetValue(MaximizeColumnTriggerProperty);

        /// <summary>
        /// set margin
        /// </summary>
        /// <param name="dependencyObject">dependency Object</param>
        /// <param name="MaximizeColumnTrigger">MaximizeColumnTrigger</param>
        public static void SetMaximizeColumnTrigger(DependencyObject dependencyObject, bool? MaximizeColumnTrigger) => dependencyObject.SetValue(MaximizeColumnTriggerProperty, MaximizeColumnTrigger);

        /// <summary>
        /// margin property
        /// </summary>
        public static readonly DependencyProperty MaximizeColumnTriggerProperty =
            DependencyProperty.RegisterAttached(
                "MaximizeColumnTrigger",
                typeof(bool?),
                typeof(Grids),
                new UIPropertyMetadata(null, MaximizeColumnTriggerChanged));

        #endregion

        static void MaximizeColumnTriggerChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs eventArgs)
        {
            if (DesignerProperties.GetIsInDesignMode(dependencyObject)) return;
            if (!(dependencyObject is Grid grid)) return;
            SetColumnMaximizedMinimizedState(grid);
        }

        static void SetColumnMaximizedMinimizedState(Grid grid)
        {
            var maxColIndex = (int)grid.GetValue(MaximizeColumnIndexProperty);
            var minColIndex = (int)grid.GetValue(MinimizeColumnIndexProperty);
            var maximizedState = grid.GetValue(MaximizeColumnTriggerProperty);
            var maximized = maximizedState == null ? true : (bool)maximizedState;
            if (maxColIndex < 0 || maxColIndex > grid.ColumnDefinitions.Count) return;
            if (minColIndex < 0 || minColIndex > grid.ColumnDefinitions.Count) return;

            var maxCol = grid.ColumnDefinitions[maxColIndex];
            var minCol = grid.ColumnDefinitions[minColIndex];
            if (!maximized)
            {
                maxCol.Width = new GridLength(100d, GridUnitType.Star);
                minCol.Width = new GridLength(0d, GridUnitType.Star);
            }
            else
            {
                maxCol.Width = new GridLength(50d, GridUnitType.Star);
                minCol.Width = new GridLength(50d, GridUnitType.Star);
            }
        }
    }
}
