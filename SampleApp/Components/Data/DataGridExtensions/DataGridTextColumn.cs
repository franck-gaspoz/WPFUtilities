using System.ComponentModel;
using System.Windows;

using WPFUtilities.Extensions.DependencyObjects;
using WPFUtilities.Extensions.Styles;

using DataGridTextColumnType = System.Windows.Controls.DataGridTextColumn;

namespace SampleApp.Components.Data.DataGridExtensions
{
    /// <summary>
    /// data grid text column extension
    /// </summary>
    public class DataGridTextColumn : DataGridTextColumnType
    {
        #region text alignment

        public TextAlignment TextAlignment
        {
            get { return (TextAlignment)GetValue(TextAlignmentProperty); }
            set { SetValue(TextAlignmentProperty, value); }
        }

        public static readonly DependencyProperty TextAlignmentProperty =
            DependencyObjectExtensions.Register("TextAlignment",
                typeof(TextAlignment), typeof(DataGridTextColumn),
                new PropertyMetadata(TextAlignment.Left, TextAlignmentChanged));

        #endregion
        private static void TextAlignmentChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs args)
        {
            if (DesignerProperties.GetIsInDesignMode(dependencyObject)
                || !(dependencyObject is DataGridTextColumn column)) return;

            var alignment = GetAlignment(column);

            var style = column.ElementStyle.MakeCopy();
            style.Setters.Add(
                new Setter(
                    FrameworkElement.HorizontalAlignmentProperty,
                    alignment));
            column.ElementStyle = style;
        }

        static HorizontalAlignment GetAlignment(DependencyObject dependencyObject)
        {
            HorizontalAlignment alignment = HorizontalAlignment.Left;
            switch (dependencyObject.GetValue(TextAlignmentProperty))
            {
                case TextAlignment.Left:
                    alignment = HorizontalAlignment.Left;
                    break;
                case TextAlignment.Justify:
                case TextAlignment.Center:
                    alignment = HorizontalAlignment.Center;
                    break;
                case TextAlignment.Right:
                    alignment = HorizontalAlignment.Right;
                    break;
            }
            return alignment;
        }
    }
}
