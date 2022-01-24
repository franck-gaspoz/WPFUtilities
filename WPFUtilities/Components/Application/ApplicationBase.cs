using System;
using System.Globalization;
using System.Windows;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using WPFUtilities.Components.Services;
using WPFUtilities.Helpers;

using syswin = System.Windows;

namespace WPFUtilities.Components.Application
{
    /// <summary>
    /// application base
    /// </summary>
    public class ApplicationBase :
        syswin.Application,
        IApplicationBase,
        IServicesConfigurator
    {
        /// <summary>
        /// application host
        /// </summary>
        public IApplicationHost ApplicationHost { get; protected set; }
            = new ApplicationHost();

        /// <summary>
        /// application base view model
        /// </summary>
        public IApplicationViewModelBase ViewModelBase { get; set; }
            = new ApplicationViewModelBase();

        /// <summary>
        /// application base settings
        /// </summary>
        public IApplicationBaseSettings ApplicationBaseSettings { get; protected set; }

        /// <summary>
        /// creates a new instance with default settings
        /// </summary>
        public ApplicationBase() { }

        /// <summary>
        /// creates a new instance with settings
        /// </summary>
        /// <param name="applicationBaseSettings"></param>
        public ApplicationBase(ApplicationBaseSettings applicationBaseSettings)
            => ApplicationBaseSettings = applicationBaseSettings;

        /// <summary>
        /// starts the UI
        /// </summary>
        void StartUI()
        {
            Func<Window> getWindow = () => (Window)ApplicationHost.Services
                .GetRequiredService(ApplicationBaseSettings.MainWindowType);

            if (ApplicationBaseSettings.MainWindowComponentType != null)
            {
                var _mainWindowComponent = ApplicationHost.Services.GetRequiredComponent(
                    ApplicationBaseSettings.MainWindowComponentType);

                getWindow = () => (Window)_mainWindowComponent.ComponentHost.Services
                    .GetService(ApplicationBaseSettings.MainWindowType);
            }

            OnStartUI();

            if (ApplicationBaseSettings.MainWindowType != null)
            {
                MainWindow = getWindow();

                InitializeMainWindow(MainWindow);

                if (ApplicationBaseSettings.ShowWindow)
                {
                    if (ApplicationBaseSettings.IsMainWindowDialog)
                        _ = MainWindow.ShowDialog();
                    else
                        MainWindow.Show();
                }
            }
        }

        /// <summary>
        /// initialize culture
        /// </summary>

        protected void InitializeCulture()
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
            try
            {
                ApplicationBaseSettings = ApplicationBaseSettings ?? new ApplicationBaseSettings();
                InitializeCulture();

                Initialize();

                ApplicationHost.Configure(ApplicationBaseSettings);

                ApplicationHost.HostBuilder
                    .ConfigureServices(
                        (context, services) => ConfigureServices(context, services));

                ApplicationHost.Build();

                await ApplicationHost.Host.StartAsync();

                StartUI();
            }
            catch (Exception exception)
            {
                OnFatalError(exception);
            }
        }

        /// <summary>
        /// called if a fatal error occurs
        /// </summary>
        /// <param name="exception">exception</param>
        protected virtual void OnFatalError(Exception exception)
        {
            UIHelper.ShowError(exception);
            Environment.Exit(1);
        }

        /// <summary>
        /// initialize before host builder
        /// </summary>
        protected virtual void Initialize() { }

        /// <inheritdoc/>
        public virtual void ConfigureServices(HostBuilderContext context, IServiceCollection services)
        {
            if (ApplicationBaseSettings.MainWindowComponentType != null)
                services.AddSingleton(ApplicationBaseSettings.MainWindowComponentType);
            else
            {
                if (ApplicationBaseSettings.MainWindowType != null)
                    services.AddSingleton(ApplicationBaseSettings.MainWindowType);
            }
        }

        /// <summary>
        /// initialize main window if any
        /// </summary>
        /// <param name="window">main window</param>
        protected virtual void InitializeMainWindow(Window window)
        {

        }

        /// <summary>
        /// terminate application
        /// </summary>
        /// <param name="eventArgs">event args</param>
        protected async override void OnExit(ExitEventArgs eventArgs)
        {
            OnShutdown();
            using (ApplicationHost.Host)
            {
                await ApplicationHost.Host.StopAsync(
                    TimeSpan.FromSeconds(
                        ApplicationBaseSettings.ShutdownTimeout));
            }
        }

        /// <summary>
        /// on shut down
        /// </summary>
        protected virtual void OnShutdown()
        {

        }

        /// <summary>
        /// on start ui
        /// </summary>
        protected virtual void OnStartUI()
        {

        }
    }
}
