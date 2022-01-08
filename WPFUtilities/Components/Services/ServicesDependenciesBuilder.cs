using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

using Microsoft.Extensions.DependencyInjection;

using WPFUtilities.Attributes;
using WPFUtilities.ComponentModels;
using WPFUtilities.Extensions.Reflections;

namespace WPFUtilities.Components.Services
{
    /// <summary>
    /// find services dependencies and add to host services collection
    /// </summary>
    public class ServicesDependenciesBuilder : IServicesDependenciesBuilder
    {
        readonly IServicesDependencyTypeResolver _servicesDependencyTypeResolver
            = new ServicesDependencyTypeResolver();

        IServiceCollection _services;

        /// <summary>
        /// creates a new instance
        /// </summary>
        /// <param name="services">services</param>
        public ServicesDependenciesBuilder(IServiceCollection services)
        {
            _services = services;
        }

        /// <inheritdoc/>
        public ServicesDependenciesBuilder AddSingletonServices()
        {
            AppDomain.CurrentDomain.GetAssemblies()
                .Where(x => x.GetCustomAttribute<DependencyServicesAttribute>() != null)
                .ToList()
                .ForEach(x => AddSingletonServices(x));
            return this;
        }

        /// <inheritdoc/>
        public ServicesDependenciesBuilder AddDependencyServices()
        {
            AppDomain.CurrentDomain.GetAssemblies()
                .Where(x => x.GetCustomAttribute<DependencyServicesAttribute>() != null)
                .ToList()
                .ForEach(x => AddDependencyServices(x));
            return this;
        }

        /// <inheritdoc/>
        public ServicesDependenciesBuilder AddDependencyServices(Assembly assembly)
        {
            var types = GetTypesWithAttribute(assembly, typeof(DependencyServiceAttribute)).ToArray();
            for (int i = 0; i < types.Length; i++)
            {
                var type = types[i];
                var attribute = type.GetCustomAttribute<DependencyServiceAttribute>();
                switch (attribute.DependencyScope)
                {
                    case DependencyScope.Singleton:
                        AddSingleton(type, attribute.ImplementationFactory);
                        break;
                    case DependencyScope.Transient:
                        AddTransient(type, attribute.ImplementationFactory);
                        break;
                    case DependencyScope.Scoped:
                        AddScoped(type, attribute.ImplementationFactory);
                        break;
                }
            }
            return this;
        }

        void AddSingleton(Type type, Func<IServiceProvider, object> implementationFactory)
        {
            var interfaceType = _servicesDependencyTypeResolver.GetDirectInterfaceType(type);
            if (implementationFactory == null)
            {
                if (interfaceType != null)
                    _services.AddSingleton(interfaceType, type);
                else
                    _services.AddSingleton(type);
            }
            else
            {
                if (interfaceType != null)
                    _services.AddSingleton(interfaceType, implementationFactory);
                else
                    _services.AddSingleton(type, implementationFactory);
            }
        }

        void AddTransient(Type type, Func<IServiceProvider, object> implementationFactory)
        {
            var interfaceType = _servicesDependencyTypeResolver.GetDirectInterfaceType(type);
            if (implementationFactory == null)
            {
                if (interfaceType != null)
                    _services.AddTransient(interfaceType, type);
                else
                    _services.AddTransient(type);
            }
            else
            {
                if (interfaceType != null)
                    _services.AddTransient(interfaceType, implementationFactory);
                else
                    _services.AddTransient(implementationFactory);
            }
        }

        void AddScoped(Type type, Func<IServiceProvider, object> implementationFactory)
        {
            var interfaceType = _servicesDependencyTypeResolver.GetDirectInterfaceType(type);
            if (implementationFactory == null)
            {
                if (interfaceType != null)
                    _services.AddScoped(interfaceType, type);
                else
                    _services.AddScoped(type);
            }
            else
            {
                if (interfaceType != null)
                    _services.AddScoped(interfaceType, implementationFactory);
                else
                    _services.AddScoped(implementationFactory);
            }
        }

        /// <inheritdoc/>
        public ServicesDependenciesBuilder AddSingletonServices(Assembly assembly)
        {
            var types = GetTypes(assembly, typeof(SingletonService<>).Name).ToArray();
            for (int i = 0; i < types.Length; i++)
                AddSingleton(types[i], null);
            return this;
        }

        List<Type> GetTypesWithAttribute(Assembly assembly, Type attributeType)
        {
            List<Type> types = new List<Type>();
            var assemblyTypes = assembly.GetTypes().ToArray();
            for (int i = 0; i < assemblyTypes.Length; i++)
            {
                var type = assemblyTypes[i];
                if (!type.IsAbstract && !type.IsInterface && type.GetCustomAttribute(attributeType) != null)
                    types.Add(type);
            }
            return types;
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
