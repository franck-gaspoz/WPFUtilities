using System.Windows;

namespace SampleApp.Components.Data.DataGridExtensions
{
    /// <summary>
    /// data grid text centered column
    /// </summary>
    public class DataGridTextCenterColumn : DataGridTextColumn
    {
        public DataGridTextCenterColumn()
        {
            TextAlignment = TextAlignment.Center;
        }
    }
}
