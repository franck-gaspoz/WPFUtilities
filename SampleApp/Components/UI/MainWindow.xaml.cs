using System.Windows;

namespace SampleApp.Components.UI
{
    /// <summary>
    /// main window controler
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        /// creates a new instance
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
            Loaded += MainWindow_Loaded;
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {

        }
    }
}
