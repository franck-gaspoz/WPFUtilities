using System.Windows;
using System.Windows.Controls;

using WPFUtilities.Extensions.DependencyObjects;

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

        public static readonly DependencyProperty TitleProperty =
            DependencyObjectExtensions.Register(
                "Title", typeof(string),
                typeof(LogView),
                new PropertyMetadata("LogView title"));

        /// <summary>
        /// creates a new instance
        /// </summary>
        public LogView()
        {
            InitializeComponent();
        }
    }
}
