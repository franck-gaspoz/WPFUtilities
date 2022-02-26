using System.ComponentModel;
using System.Windows;

using win = System.Windows;

namespace WPFUtilities.Components.UI
{
    /// <summary>
    /// restore initial location on show
    /// </summary>
    public static partial class Window
    {
        #region center on parent on show

        /// <summary>
        /// get restore initial location on show
        /// </summary>
        /// <param name="dependencyObject">dependency object</param>
        /// <returns>value</returns>
        public static bool GetCenterOnParentOnShow(DependencyObject dependencyObject) => (bool)dependencyObject.GetValue(CenterOnParentOnShowProperty);

        /// <summary>
        /// set restore initial location on show
        /// </summary>
        /// <param name="dependencyObject">dependency Object</param>
        /// <param name="value">value</param>
        public static void SetCenterOnParentOnShow(DependencyObject dependencyObject, bool value) => dependencyObject.SetValue(CenterOnParentOnShowProperty, value);

        /// <summary>
        /// restore initial location on show property
        /// </summary>
        public static readonly DependencyProperty CenterOnParentOnShowProperty =
            DependencyProperty.Register(
                "CenterOnParentOnShow",
                typeof(bool),
                typeof(win.Window),
                new UIPropertyMetadata(false, CenterOnParentOnShowChanged));

        static void CenterOnParentOnShowChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs eventArgs)
        {
            if (DesignerProperties.GetIsInDesignMode(dependencyObject)) return;
            if (!(dependencyObject is win.Window window)) return;

            if ((bool)eventArgs.NewValue)
                window.IsVisibleChanged += Window_IsVisibleChanged_CenterOnParent;
            else
                window.IsVisibleChanged -= Window_IsVisibleChanged_CenterOnParent;
        }

        #endregion

        private static void Window_IsVisibleChanged_CenterOnParent(object sender, DependencyPropertyChangedEventArgs e)
        {
            if ((bool)e.NewValue && sender is win.Window win)
            {
                if (!(win.Owner is win.Window owner)) return;

                var deltaWidth = owner.Width - win.Width;
                var deltaHeight = owner.Height - win.Height;
                var left = owner.Left + deltaWidth / 2d;
                var top = owner.Top + deltaHeight / 2d;
                win.Left = left;
                win.Top = top;
            }
        }
    }
}
