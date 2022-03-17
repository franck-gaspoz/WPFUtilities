using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

using WPFUtilities.Extensions.DependencyObjects;

namespace SampleApp.Components.Data.Tree
{
    /// <summary>
    /// will bind a column tree of the tree datagrid cell data template
    /// <para>from TreeColumnName dp of UserControl parent first, or DataGrid control second</para>
    /// </summary>
    public static class TreeColumnBinding
    {
        #region target dp

        /// <summary>
        /// get DependencyProperty
        /// </summary>
        /// <param name="dependencyObject">dependency object</param>
        /// <returns>value</returns>
        public static DependencyProperty GetTarget(DependencyObject dependencyObject) => (DependencyProperty)dependencyObject.GetValue(TargetProperty);

        /// <summary>
        /// set DependencyProperty
        /// </summary>
        /// <param name="dependencyObject">dependency Object</param>
        /// <param name="Target">Target</param>
        public static void SetTarget(DependencyObject dependencyObject, DependencyProperty Target) => dependencyObject.SetValue(TargetProperty, Target);

        /// <summary>
        /// DependencyProperty property
        /// </summary>
        public static readonly DependencyProperty TargetProperty =
            DependencyObjectExtensions.Register(
                "Target",
                typeof(DependencyProperty),
                typeof(DependencyObject),
                new UIPropertyMetadata(null, TargetChanged));

        #endregion

        static void TargetChanged(DependencyObject obj, DependencyPropertyChangedEventArgs eventArgs)
        {
            if (DesignerProperties.GetIsInDesignMode(obj)) return;
            Initialize(obj);
        }

        static void Initialize(DependencyObject obj)
        {
            var target = obj.GetValue<DependencyProperty>(TargetProperty);
            SetBinding(obj);
        }

        static void SetBinding(DependencyObject obj)
        {
            var uc = WPFUtilities.Helpers.WPFHelper.FindVisualAncestor<UserControl>(obj);
            var dg = WPFUtilities.Helpers.WPFHelper.FindVisualAncestor<DataGrid>(obj);

            var val0 = uc?.GetValue<string>(WPFUtilities.Components.UI.DataGrid.TreeColumnPathProperty);
            var val = dg?.GetValue<string>(WPFUtilities.Components.UI.DataGrid.TreeColumnPathProperty);
            var path = val0 ?? val;
            var target = GetTarget(obj);

            var binding = new Binding(path);
            BindingOperations.SetBinding(obj, target, binding);
        }
    }
}