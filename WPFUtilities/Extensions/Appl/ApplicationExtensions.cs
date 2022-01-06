using System.Windows;

using WPFUtilities.Components.Appl;
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
        public static IApplicationBase App(this object obj) => (IApplicationBase)Application.Current;

        /// <summary>
        /// current application view model
        /// </summary>
        public static IAppViewModelBase ViewModel(this Application app) => (app as IApplicationBase)?.ViewModelBase;
    }
}
