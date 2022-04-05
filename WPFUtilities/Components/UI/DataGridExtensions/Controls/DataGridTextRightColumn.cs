using System.Windows;

namespace WPFUtilities.Components.UI.DataGridExtensions.Controls
{
    /// <summary>
    /// data grid text on right column
    /// </summary>
    public class DataGridTextRightColumn : DataGridTextColumn
    {
        /// <summary>
        /// creates a new instance
        /// </summary>
        public DataGridTextRightColumn()
        {
            TextAlignment = TextAlignment.Right;
        }
    }
}
