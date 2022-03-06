using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;

namespace SampleApp.Components.Data.Tree
{
    /// <summary>
    /// Logique d'interaction pour TreeDataGridView.xaml
    /// </summary>
    public partial class TreeDataGridView : UserControl
    {
        public TreeDataGridView()
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
                typeof(TreeDataGridView),
                new PropertyMetadata(new ObservableCollection<DataGridColumn>()));

        public object TreeColumnHeader
        {
            get { return (object)GetValue(TreeColumnHeaderProperty); }
            set { SetValue(TreeColumnHeaderProperty, value); }
        }

        public static readonly DependencyProperty TreeColumnHeaderProperty =
            DependencyProperty.Register(
                "TreeColumnHeader",
                typeof(object),
                typeof(TreeDataGridView),
                new PropertyMetadata(null));
    }
}
