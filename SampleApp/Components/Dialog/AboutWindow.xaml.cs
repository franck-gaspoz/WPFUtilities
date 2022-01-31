using System.Windows;

using WPFUtilities.Components.Services;

namespace SampleApp.Components.Dialog
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
