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
        /// host
        /// </summary>
        protected IHost Host;

        IApplicationBaseSettings _applicationBaseSettings;

        /// <summary>
        /// creates a new instance
        /// </summary>
        public ApplicationBase() { }

        /// <summary>
        /// launch the application, catch any exception and inform about it before terminating the app
        /// </summary>
        /// <param name="applicationBaseSettings">application base settings</param>
        public ApplicationBase(IApplicationBaseSettings applicationBaseSettings)
            => Start(applicationBaseSettings);

        /// <summary>
        /// starts the application
        /// </summary>
        /// <param name="applicationBaseSettings">application base settings</param>
        protected void Start(IApplicationBaseSettings applicationBaseSettings)
        {
            try
            {
                _applicationBaseSettings = applicationBaseSettings;

                if (applicationBaseSettings.DefaultCulture != null)
                    CultureInfo.DefaultThreadCurrentCulture =
                    CultureInfo.DefaultThreadCurrentUICulture =
                        new CultureInfo(applicationBaseSettings.DefaultCulture);

                BuildHost(applicationBaseSettings);

                applicationBaseSettings.Initialize?.Invoke();

                MainWindow = (Window)Activator.CreateInstance(applicationBaseSettings.MainWindowType);

                applicationBaseSettings.InitializeWindow?.Invoke(MainWindow);

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

        /// <summary>
        /// build host
        /// </summary>
        /// <param name="applicationBaseSettings">application base settings</param>
        void BuildHost(IApplicationBaseSettings applicationBaseSettings)
        {
            var hostBuilder = new HostBuilder()
                    .ConfigureServices(configureServices);
            if (_applicationBaseSettings.ConfigureServices != null)
                hostBuilder.ConfigureServices(_applicationBaseSettings.ConfigureServices);
            Host = hostBuilder.Build();
        }

        /// <summary>
        /// setup default services
        /// </summary>
        /// <param name="services">services</param>
        void configureServices(IServiceCollection services)
        {

        }

        /// <summary>
        /// terminate application
        /// </summary>
        /// <param name="eventArgs">event args</param>
        protected async override void OnExit(ExitEventArgs eventArgs)
        {
            using (Host)
            {
                await Host.StopAsync(
                    TimeSpan.FromSeconds(
                        _applicationBaseSettings.ShutdownTimeout));
            }
        }
    }
}
