
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

using WPFUtilities.Attributes;
using WPFUtilities.ComponentModels;
using WPFUtilities.Extensions.Reflections;

namespace WPFUtilities.Components.Appl
{
    /// <summary>
    /// application host
    /// </summary>
    public class ApplicationHost : IApplicationHost
    {
        #region singleton pattern

        /// <summary>
        /// private constructor 
        /// </summary>
        ApplicationHost() { }

        static object _lock = new object();

        static ApplicationHost _instance;
        /// <summary>
        /// singleton instance
        /// </summary>
        public static ApplicationHost Instance
        {
            get
            {
                lock (_lock)
                {
                    if (_instance == null)
                        _instance = new ApplicationHost();
                    return _instance;
                }
            }
        }

        #endregion

        /// <inheritdoc/>
        public IHost Host { get; private set; }

        /// <inheritdoc/>
        public IHostBuilder HostBuilder { get; private set; }

        IApplicationBaseSettings _applicationBaseSettings;

        /// <inheritdoc/>
        public void Configure(IApplicationBaseSettings applicationBaseSettings)
        {
            _applicationBaseSettings = applicationBaseSettings;

            HostBuilder = new HostBuilder()
                .ConfigureLogging(configureLogging)
                .ConfigureServices(configureServices);

            applicationBaseSettings.InitializeHost?.Invoke(HostBuilder);

            if (applicationBaseSettings.ConfigureServices != null)
                HostBuilder.ConfigureServices(applicationBaseSettings.ConfigureServices);
        }

        /// <summary>
        /// build the host
        /// </summary>
        public void Build()
        {
            Host = HostBuilder.Build();
        }

        /// <summary>
        /// configure logging
        /// </summary>
        /// <param name="loggingBuilder">logging builder</param>
        private void configureLogging(ILoggingBuilder loggingBuilder)
        {
            loggingBuilder.ClearProviders();
            loggingBuilder.SetMinimumLevel(_applicationBaseSettings.ApplicationLoggingSettings.MinimumLogLevel);
        }

        /// <summary>
        /// setup default services
        /// </summary>
        /// <param name="services">services</param>
        void configureServices(IServiceCollection services)
        {
            AddSingletonServices(services);
        }

        /// <summary>
        /// add singleton services to service collection
        /// </summary>
        /// <param name="services">services</param>
        public void AddSingletonServices(IServiceCollection services)
            => AppDomain.CurrentDomain.GetAssemblies()
                    .Where(x => x.GetCustomAttribute<InjectableServicesAttribute>() != null)
                    .ToList()
                    .ForEach(x => AddSingletonServices(services, x));

        /// <summary>
        /// add singleton services to service collection from types in the given assembly
        /// </summary>
        /// <param name="services">services</param>
        /// <param name="assembly">assembly</param>
        public void AddSingletonServices(IServiceCollection services, Assembly assembly)
        {
            var types = GetTypes(assembly, typeof(SingletonService<>).Name).ToArray();
            for (int i = 0; i < types.Length; i++)
                services.AddSingleton(types[i]);
        }

        List<Type> GetTypes(Assembly assembly, string inheritsFromTypeName)
        {
            List<Type> types = new List<Type>();
            var assemblyTypes = assembly.GetTypes().ToArray();
            for (int i = 0; i < assemblyTypes.Length; i++)
            {
                var type = assemblyTypes[i];
                if (!type.IsAbstract && !type.IsInterface && type.InheritsFrom(inheritsFromTypeName))
                    types.Add(type);
            }
            return types;
        }
    }
}
