using System;
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
        public MainWindow(IServiceProvider serviceProvider)
        {
            InitializeComponent();
        }
    }
}
