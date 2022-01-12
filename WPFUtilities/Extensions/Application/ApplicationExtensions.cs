using System.Windows;

using WPFUtilities.Components.Application;

namespace WPFUtilities.Extensions.App
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
        public static IApplicationViewModelBase ViewModel(this Application app) => (app as IApplicationBase)?.ViewModelBase;
    }
}
