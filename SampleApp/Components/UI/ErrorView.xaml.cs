using System.Windows;
using System.Windows.Controls;

namespace SampleApp.Components.UI
{
    /// <summary>
    /// Logique d'interaction pour ErrorView.xaml
    /// </summary>
    public partial class ErrorView : UserControl
    {
        public ErrorView()
        {
            InitializeComponent();
        }

        public string Error
        {
            get { return (string)GetValue(ErrorProperty); }
            set { SetValue(ErrorProperty, value); }
        }

        public static readonly DependencyProperty ErrorProperty =
            DependencyProperty.Register(
                "Error",
                typeof(string),
                typeof(ErrorView),
                new PropertyMetadata(null));
    }
}
