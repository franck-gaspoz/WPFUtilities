using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;

namespace SampleApp.Components.Data.Tree
{
    /// <summary>
    /// Logique d'interaction pour HostsView.xaml
    /// </summary>
    public partial class TreeDataGridRowView : UserControl
    {
        public TreeDataGridRowView()
        {
            InitializeComponent();
        }

        public ObservableCollection<DataGridColumn> Columns
        {
            get { return (ObservableCollection<DataGridColumn>)GetValue(ColumnsProperty); }
            set { SetValue(ColumnsProperty, value); }
        }

        public static readonly DependencyProperty ColumnsProperty =
            DependencyProperty.Register(
                "Columns",
                typeof(ObservableCollection<DataGridColumn>),
                typeof(TreeDataGridRowView),
                new PropertyMetadata(null));

    }
}
