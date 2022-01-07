﻿using System;
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
                        _services.AddSingleton(type);
                        break;
                    case DependencyScope.Transient:
                        _services.AddTransient(type);
                        break;
                    case DependencyScope.Scoped:
                        _services.AddScoped(type);
                        break;
                }
            }
            return this;
        }

        /// <inheritdoc/>
        public ServicesDependenciesBuilder AddSingletonServices(Assembly assembly)
        {
            var types = GetTypes(assembly, typeof(SingletonService<>).Name).ToArray();
            for (int i = 0; i < types.Length; i++)
                _services.AddSingleton(types[i]);
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
