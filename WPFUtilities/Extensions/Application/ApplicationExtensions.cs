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
        public static IApplicationBase GetApplication(this object obj) => Application.Current as IApplicationBase;

        /// <summary>
        /// current application view model
        /// </summary>
        public static IApplicationViewModelBase GetApplicationViewModel(this Application app) => (app as IApplicationBase)?.ViewModelBase;
    }
}
