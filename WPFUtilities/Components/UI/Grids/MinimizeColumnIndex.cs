using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;

namespace WPFUtilities.Components.UI
{
    /// <summary>
    /// minimize the grid column of index depending on boolean trigger
    /// </summary>
    public static partial class Grids
    {
        #region MinimizeColumnIndexChanged

        /// <summary>
        /// get margin
        /// </summary>
        /// <param name="dependencyObject">dependency object</param>
        /// <returns>value</returns>
        public static int GetMinimizeColumnIndex(DependencyObject dependencyObject) => (int)dependencyObject.GetValue(MinimizeColumnIndexProperty);

        /// <summary>
        /// set margin
        /// </summary>
        /// <param name="dependencyObject">dependency Object</param>
        /// <param name="MinimizeColumnIndex">MinimizeColumnIndex</param>
        public static void SetMinimizeColumnIndex(DependencyObject dependencyObject, int MinimizeColumnIndex) => dependencyObject.SetValue(MinimizeColumnIndexProperty, MinimizeColumnIndex);

        /// <summary>
        /// margin property
        /// </summary>
        public static readonly DependencyProperty MinimizeColumnIndexProperty =
            DependencyProperty.RegisterAttached(
                "MinimizeColumnIndex",
                typeof(int),
                typeof(Grids),
                new UIPropertyMetadata(0, MinimizeColumnIndexChanged));

        #endregion

        static void MinimizeColumnIndexChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs eventArgs)
        {
            if (DesignerProperties.GetIsInDesignMode(dependencyObject)) return;
            if (!(dependencyObject is Grid grid)) return;
            SetColumnMaximizedMinimizedState(grid);
        }
    }
}
