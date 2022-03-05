using System;
using System.Windows;

using WPFUtilities.Components.ServiceComponent;

namespace WPFUtilities.Components.Services.Properties
{
    /// <summary>
    /// service dependency data context stateless behavior: setup data context from dependency injector
    /// <para>explicit resolve: {ResolveProperty} -di-> {ResolveProperty} </para>
    /// <para>default resolve: {Name}View -> I{Name}ViewModel , Name{ViewModel} </para>
    /// <para>fallback resolve: {Name} -> I{Name}ViewModel -di-> Name{ViewModel} </para>
    /// </summary>
    public static class DataContextResolveSetter
    {
        static readonly IServicesDependencyTypeResolver _servicesDependencyTypeResolver
            = new ServicesDependencyTypeResolver();

        /// <summary>
        /// setup service dependency data context behavior: setup data context from dependency injector
        /// <para>Resolve="{x:Type ..}"</para>
        /// <para>resolve a view model from resolve type</para>
        /// </summary>
        /// <param name="dependencyObject">dependency object</param>
        /// <param name="type">resolve for the given type</param>
        /// <param name="getComponentHost">func that provides the component host</param>
        public static void SetupServiceDependencyDataContext(
            DependencyObject dependencyObject,
            Type type,
            Func<DependencyObject, IComponentHost> getComponentHost)
        {
            var componentHost = getComponentHost(dependencyObject);
            if (dependencyObject is FrameworkElement frameworkElement
                && type != null
                && componentHost != null)
            {
                frameworkElement.DataContext =
                    componentHost.Services
                        .GetService(type);
            }
        }

        /// <summary>
        /// setup service dependency data context behavior: setup data context from dependency injector
        /// <para>IsAuto="True"</para>
        /// <para>resolve a view model from naming rules IServicesDependencyTypeResolver</para>
        /// </summary>
        /// <param name="dependencyObject">dependency object</param>
        /// <param name="getComponentHost">func that provides the component host</param>
        public static void SetupServiceDependencyDataContext(
            DependencyObject dependencyObject,
            Func<DependencyObject, IComponentHost> getComponentHost
            )
        {
            var componentHost = getComponentHost(dependencyObject);
            if (dependencyObject is FrameworkElement frameworkElement
                && componentHost != null)
            {
                // interface lookup

                var type = dependencyObject.GetType();
                var interfaceType = _servicesDependencyTypeResolver
                    .GetViewModelInterfaceType(dependencyObject.GetType());
                if (interfaceType != null)
                    frameworkElement.DataContext =
                        // TODO: clean up runtime dependencies error and ask for a required service here
                        componentHost.Services
                        .GetService(interfaceType);
            }
        }
    }
}