using System.Windows;

namespace WPFUtilities.Components.UI.DataGridExtensions.Controls
{
    /// <summary>
    /// data grid text centered column
    /// </summary>
    public class DataGridTextCenterColumn : DataGridTextColumn
    {
        /// <summary>
        /// creates a new instance
        /// </summary>
        public DataGridTextCenterColumn()
        {
            TextAlignment = TextAlignment.Center;
        }
    }
}
