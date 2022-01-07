using System;
using System.Globalization;
using System.Windows;

using Microsoft.Extensions.DependencyInjection;
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
        public ApplicationBase() { }

        /// <summary>
        /// starts the UI
        /// </summary>
        void StartUI()
        {
            try
            {
                if (ApplicationBaseSettings.MainWindowType != null)
                {
                    MainWindow = (Window)ApplicationHost.Instance.Host.Services
                        .GetService(ApplicationBaseSettings.MainWindowType);

                    ApplicationBaseSettings.InitializeMainWindow?.Invoke(MainWindow);

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
        /// initialize culture
        /// </summary>

        void InitializeCulture()
        {
            if (ApplicationBaseSettings.DefaultCulture != null)
                CultureInfo.DefaultThreadCurrentCulture =
                CultureInfo.DefaultThreadCurrentUICulture =
                    new CultureInfo(ApplicationBaseSettings.DefaultCulture);
        }

        /// <summary>
        /// startup application
        /// </summary>
        /// <param name="eventArgs">event args</param>
        protected async override void OnStartup(StartupEventArgs eventArgs)
        {
            ApplicationBaseSettings = ApplicationBaseSettings ?? new ApplicationBaseSettings();
            InitializeCulture();
            ApplicationBaseSettings.Initialize?.Invoke();
            ApplicationHost.Instance.Configure(ApplicationBaseSettings);
            if (ApplicationBaseSettings.MainWindowType != null)
                ApplicationHost.Instance.HostBuilder
                    .ConfigureServices((services) =>
                    {
                        services.AddTransient(ApplicationBaseSettings.MainWindowType);
                    });
            ApplicationHost.Instance.Build();
            await ApplicationHost.Instance.Host.StartAsync();
            StartUI();
        }

        /// <summary>
        /// terminate application
        /// </summary>
        /// <param name="eventArgs">event args</param>
        protected async override void OnExit(ExitEventArgs eventArgs)
        {
            ApplicationBaseSettings.ShutdownAction?.Invoke();
            using (ApplicationHost.Instance.Host)
            {
                await ApplicationHost.Instance.Host.StopAsync(
                    TimeSpan.FromSeconds(
                        ApplicationBaseSettings.ShutdownTimeout));
            }
        }
    }
}
