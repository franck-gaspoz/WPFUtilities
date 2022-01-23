using System.Windows;
using System.Windows.Controls;

namespace SampleApp.Components.Logging
{
    /// <summary>
    /// LogView controler
    /// </summary>    
    public partial class LogView : UserControl
    {
        public string Title
        {
            get { return (string)GetValue(TitleProperty); }
            set { SetValue(TitleProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Title.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TitleProperty =
            DependencyProperty.Register("Title", typeof(string), typeof(LogView), new PropertyMetadata("LogView title"));

        /// <summary>
        /// creates a new instance
        /// </summary>
        public LogView()
        {
            InitializeComponent();
        }
    }
}
