using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;

namespace WPFUtilities.Components.UI
{
    /// <summary>
    /// maximize the grid column of index depending on boolean trigger
    /// </summary>
    public static partial class Grids
    {
        #region MaximizeColumnIndexChanged

        /// <summary>
        /// get margin
        /// </summary>
        /// <param name="dependencyObject">dependency object</param>
        /// <returns>value</returns>
        public static int GetMaximizeColumnIndex(DependencyObject dependencyObject) => (int)dependencyObject.GetValue(MaximizeColumnIndexProperty);

        /// <summary>
        /// set margin
        /// </summary>
        /// <param name="dependencyObject">dependency Object</param>
        /// <param name="MaximizeColumnIndex">MaximizeColumnIndex</param>
        public static void SetMaximizeColumnIndex(DependencyObject dependencyObject, int MaximizeColumnIndex) => dependencyObject.SetValue(MaximizeColumnIndexProperty, MaximizeColumnIndex);

        /// <summary>
        /// margin property
        /// </summary>
        public static readonly DependencyProperty MaximizeColumnIndexProperty =
            DependencyProperty.RegisterAttached(
                "MaximizeColumnIndex",
                typeof(int),
                typeof(Grids),
                new UIPropertyMetadata(0, MaximizeColumnIndexChanged));

        #endregion

        static void MaximizeColumnIndexChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs eventArgs)
        {
            if (DesignerProperties.GetIsInDesignMode(dependencyObject)) return;
            if (!(dependencyObject is Grid grid)) return;
            SetColumnMaximizedMinimizedState(grid);
        }
    }
}
