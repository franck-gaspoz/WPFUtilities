using System.Windows;

using WPFUtilities.Components;
using WPFUtilities.ViewModels;

namespace WPFUtilities.Extensions.Appl
{
    /// <summary>
    /// application extensions
    /// </summary>
    public static class ApplicationExtensions
    {
        /// <summary>
        /// current application facade
        /// </summary>
        public static IApp App => Application.Current as IApp;

        /// <summary>
        /// current application view model
        /// </summary>
        public static IAppViewModel AppViewModel => (Application.Current as IApp)?.ViewModel;
    }
}
