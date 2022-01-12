using System;
using System.Globalization;
using System.Windows;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using WPFUtilities.ComponentModels;
using WPFUtilities.Components.Component;
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
        IHostServicesConfigurator
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
                IServiceProvider serviceProvider = ApplicationHost.Host.Services;

                if (ApplicationBaseSettings.MainWindowComponentType != null)
                {
                    var _mainWindowComponent = (IServiceComponent)ApplicationHost.Host.Services
                        .GetRequiredService(ApplicationBaseSettings.MainWindowComponentType);
                    _mainWindowComponent.ConfigureServices();
                    _mainWindowComponent.Build();
                    //var _mainWindowComponent = ApplicationHost.Serv
                    serviceProvider = _mainWindowComponent.ComponentHost.Host.Services;
                }

                OnStartUI();

                if (ApplicationBaseSettings.MainWindowType != null)
                {
                    MainWindow = (Window)serviceProvider
                        .GetService(ApplicationBaseSettings.MainWindowType);

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
            catch (Exception exception)
            {
                UIHelper.ShowError(exception);
                Environment.Exit(1);
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

        /// <summary>
        /// initialize before host builder [init step 1]
        /// </summary>
        protected virtual void Initialize()
        {

        }

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
