using System.Windows;

namespace SampleApp.Components.Data.DataGridExtensions
{
    /// <summary>
    /// data grid text on left column
    /// </summary>
    public class DataGridTexxtLeftColumn : DataGridTextColumn
    {
        public DataGridTexxtLeftColumn()
        {
            TextAlignment = TextAlignment.Left;
        }
    }
}
