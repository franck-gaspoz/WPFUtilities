using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

using WPFUtilities.Extensions.FrameworkElements;

namespace WPFUtilities.Extensions.DataGrids
{
    /// <summary>
    /// TODO: maybe remove - check usage
    /// bindable datagrid template column
    /// </summary>
    public class DataGridBoundTemplateColumn : DataGridBoundColumn
    {
        /// <summary>
        /// cell template
        /// </summary>
        public DataTemplate CellTemplate { get; set; }

        /// <summary>
        /// cell editing template
        /// </summary>
        public DataTemplate CellEditingTemplate { get; set; }

        /// <inheritdoc/>
        protected override System.Windows.FrameworkElement GenerateElement(DataGridCell cell, object dataItem)
        {
            return Generate(dataItem, CellTemplate);
        }

        /// <inheritdoc/>
        protected override System.Windows.FrameworkElement GenerateEditingElement(DataGridCell cell, object dataItem)
        {
            return Generate(dataItem, CellEditingTemplate);
        }

        private FrameworkElement Generate(object dataItem, DataTemplate template)
        {
            var contentControl = new ContentControl
            {
                ContentTemplate = template,
                Content = dataItem
            };
            if (Binding != null)
                BindingOperations.SetBinding(contentControl, ContentControl.ContentProperty, Binding);

            contentControl.OnLoaded((routed) =>
            {
                var control = contentControl;
                var binding = Binding;
            });

            return contentControl;
        }
    }
}