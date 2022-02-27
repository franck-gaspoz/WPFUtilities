using System.Windows;

using WPFUtilities.Components.Services;

namespace SampleApp.Components.About
{
    /// <summary>
    /// AboutWindow controller
    /// </summary>
    [ServiceDependency]
    public partial class AboutWindow : Window
    {
        public AboutWindow()
        {
            InitializeComponent();
        }
    }
}
