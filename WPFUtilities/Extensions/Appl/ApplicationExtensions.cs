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
        public static IApp App(this object obj) => (IApp)Application.Current;

        /// <summary>
        /// current application view model
        /// </summary>
        public static IAppBaseViewModel ViewModel(this Application app) => (app as IApp)?.ViewModel;
    }
}
