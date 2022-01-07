using System;
using System.Globalization;
using System.Windows;

using Microsoft.Extensions.Hosting;

using WPFUtilities.Helpers;

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
        /// application base settings
        /// </summary>
        protected IApplicationBaseSettings ApplicationBaseSettings;

        /// <summary>
        /// creates a new instance
        /// </summary>
        public ApplicationBase()
        {
        }

        /// <summary>
        /// starts the application
        /// </summary>
        protected void Start()
        {
            try
            {
                if (ApplicationBaseSettings.DefaultCulture != null)
                    CultureInfo.DefaultThreadCurrentCulture =
                    CultureInfo.DefaultThreadCurrentUICulture =
                        new CultureInfo(ApplicationBaseSettings.DefaultCulture);

                ApplicationBaseSettings.Initialize?.Invoke();

                if (ApplicationBaseSettings.MainWindowType != null)
                {
                    MainWindow = (Window)Activator.CreateInstance(ApplicationBaseSettings.MainWindowType);

                    ApplicationBaseSettings.InitializeWindow?.Invoke(MainWindow);

                    if (ApplicationBaseSettings.ShowWindow)
                    {
                        if (ApplicationBaseSettings.IsMainWindowDialog)
                            _ = MainWindow.ShowDialog();
                        else
                            MainWindow.Show();
                    }
                }
            }
            catch (Exception exception)
            {
                UIHelper.ShowError(exception);
                Environment.Exit(1);
            }
        }

        /// <summary>
        /// startup application
        /// </summary>
        /// <param name="eventArgs">event args</param>
        protected async override void OnStartup(StartupEventArgs eventArgs)
        {
            ApplicationHost.Instance.Build(ApplicationBaseSettings);
            await ApplicationHost.Instance.Host.StartAsync();
            Start();
        }

        /// <summary>
        /// terminate application
        /// </summary>
        /// <param name="eventArgs">event args</param>
        protected async override void OnExit(ExitEventArgs eventArgs)
        {
            using (ApplicationHost.Instance.Host)
            {
                await ApplicationHost.Instance.Host.StopAsync(
                    TimeSpan.FromSeconds(
                        ApplicationBaseSettings.ShutdownTimeout));
            }
        }
    }
}
