using System.Windows;

namespace SampleApp.Components.Data.DataGridExtensions
{
    /// <summary>
    /// data grid text on right column
    /// </summary>
    public class DataGridTextRightColumn : DataGridTextColumn
    {
        public DataGridTextRightColumn()
        {
            TextAlignment = TextAlignment.Right;
        }
    }
}
