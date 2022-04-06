using System.ComponentModel;
using System.Windows;

using WPFUtilities.Extensions.DependencyObjects;
using WPFUtilities.Extensions.Styles;

using DataGridTextColumnType = System.Windows.Controls.DataGridTextColumn;

namespace WPFUtilities.Components.UI.DataGridExtensions.Controls
{
    /// <summary>
    /// data grid text column extension
    /// </summary>
    public class DataGridTextColumn : DataGridTextColumnType, IDataGridNamedColumn
    {
        #region text alignment

        /// <summary>
        /// text alignment
        /// </summary>
        public TextAlignment TextAlignment
        {
            get { return (TextAlignment)GetValue(TextAlignmentProperty); }
            set { SetValue(TextAlignmentProperty, value); }
        }

        /// <summary>
        /// text alignment property
        /// </summary>
        public static readonly DependencyProperty TextAlignmentProperty =
            DependencyObjectExtensions.Register("TextAlignment",
                typeof(TextAlignment), typeof(DataGridTextColumn),
                new PropertyMetadata(TextAlignment.Left, TextAlignmentChanged));

        #endregion

        #region name

        /// <summary>
        /// column name
        /// </summary>
        public string Name
        {
            get { return (string)GetValue(NameProperty); }
            set { SetValue(NameProperty, value); }
        }

        /// <summary>
        /// name property
        /// </summary>
        public static readonly DependencyProperty NameProperty =
            DependencyObjectExtensions.Register(
                "Name",
                typeof(string),
                typeof(DataGridTextColumn),
                new PropertyMetadata(null));

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
