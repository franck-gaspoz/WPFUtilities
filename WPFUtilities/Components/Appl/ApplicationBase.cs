using System;
using System.Globalization;
using System.Windows;

using WPFUtilities.Helpers;
using WPFUtilities.ViewModels;

namespace WPFUtilities.Components.Appl
{
    /// <summary>
    /// application base
    /// </summary>
    public class ApplicationBase : Application, IApplicationBase
    {
        /// <summary>
        /// application base view model
        /// </summary>
        public IAppViewModelBase ViewModelBase { get; set; } = new AppViewModelBase();

        /// <summary>
        /// launch the application, catch any exception and inform about it before terminating the app
        /// </summary>
        /// <param name="applicationBaseSettings">application base settings</param>
        public ApplicationBase(IApplicationBaseSettings applicationBaseSettings)
        {
            try
            {
                if (applicationBaseSettings.DefaultCulture != null)
                    CultureInfo.DefaultThreadCurrentCulture =
                    CultureInfo.DefaultThreadCurrentUICulture =
                        new CultureInfo(applicationBaseSettings.DefaultCulture);

                MainWindow = (Window)Activator.CreateInstance(applicationBaseSettings.MainWindowType);
                if (applicationBaseSettings.ShowWindow)
                {
                    if (applicationBaseSettings.IsMainWindowDialog)
                        _ = MainWindow.ShowDialog();
                    else
                        MainWindow.Show();
                }
            }
            catch (Exception exception)
            {
                UIHelper.ShowError(exception);
                Environment.Exit(1);
            }
        }
    }
}
