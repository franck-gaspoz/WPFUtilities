using System;
using System.Windows;

using Microsoft.Xaml.Behaviors;

using WPFUtilities.Components.Appl;
using WPFUtilities.Components.Services;

namespace WPFUtilities.Behaviors.Services
{
    /// <summary>
    /// service dependency data context stateless behavior: setup data context from dependency injector
    /// <para>explicit resolve: {ResolveProperty} -di-> {ResolveProperty} </para>
    /// <para>default resolve: {Name}View -> I{Name}ViewModel , Name{ViewModel} </para>
    /// <para>fallback resolve: {Name} -> I{Name}ViewModel -di-> Name{ViewModel} </para>
    /// </summary>
    public class ServiceDependencyDataContextBehavior : Behavior<FrameworkElement>
    {
        #region properties

        #region IsEnabled

        /// <summary>
        /// get IsEnabled dependency property value for object
        /// </summary>
        /// <param name="dependencyObject">dependency object</param>
        /// <returns>true if is enabled</returns>
        public static bool GetIsEnabled(DependencyObject dependencyObject)
            => (bool)dependencyObject.GetValue(IsEnabledProperty);

        /// <summary>
        /// set IsEnabled dependency property value for object
        /// </summary>
        /// <param name="dependencyObject">dependency object</param>
        /// <param name="value">value</param>
        public static void SetIsEnabled(DependencyObject dependencyObject, bool value)
            => dependencyObject.SetValue(IsEnabledProperty, value);

        /// <summary>
        /// IsEnabled dependency property
        /// </summary>
        public static readonly DependencyProperty IsEnabledProperty =
            DependencyProperty.RegisterAttached(
                "IsEnabled",
                typeof(bool),
                typeof(ServiceDependencyDataContextBehavior),
                new PropertyMetadata(false, IsEnabledChanged));

        #endregion

        #region Resolve

        /// <summary>
        /// get Resolve dependency property value for object
        /// </summary>
        /// <param name="dependencyObject">dependency object</param>
        /// <returns>true if is enabled</returns>
        public static Type GetResolve(DependencyObject dependencyObject)
            => (Type)dependencyObject.GetValue(ResolveProperty);

        /// <summary>
        /// set Resolve dependency property value for object
        /// </summary>
        /// <param name="dependencyObject">dependency object</param>
        /// <param name="value">value</param>
        public static void SetResolve(DependencyObject dependencyObject, Type value)
            => dependencyObject.SetValue(ResolveProperty, value);

        /// <summary>
        /// Resolve dependency property
        /// </summary>
        public static readonly DependencyProperty ResolveProperty =
            DependencyProperty.RegisterAttached(
                "Resolve",
                typeof(Type),
                typeof(ServiceDependencyDataContextBehavior),
                new PropertyMetadata(null, ResolveChanged));

        #endregion

        static readonly IServicesDependencyTypeResolver _servicesDependencyTypeResolver
            = new ServicesDependencyTypeResolver();

        #endregion

        #region interactivity

        /// <summary>
        /// trigger setup data context when resolve is set
        /// </summary>
        /// <param name="dependencyObject">dependency object</param>
        /// <param name="eventArgs">event args</param>
        static void ResolveChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs eventArgs)
        {
            if (!(dependencyObject is FrameworkElement target)) return;
            if (eventArgs.NewValue is Type type)
                SetupServiceDependencyDataContext(dependencyObject, type);
        }

        /// <summary>
        /// trigger setup data context when is enabled is set to true
        /// </summary>
        /// <param name="dependencyObject">dependency object</param>
        /// <param name="eventArgs">event args</param>
        static void IsEnabledChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs eventArgs)
        {
            if (!(dependencyObject is FrameworkElement target)) return;
            if ((bool)eventArgs.NewValue)
                SetupServiceDependencyDataContext(dependencyObject);
        }

        #endregion

        /// <summary>
        /// setup service dependency data context behavior: setup data context from dependency injector
        /// <para>wpfubehaviorsServices:ServiceDependencyDataContextBehavior.Resolve="{x:Type ..}"</para>
        /// <para>resolve a view model</para>
        /// </summary>
        /// <param name="dependencyObject">dependency object</param>
        /// <param name="type">resolve for the given type</param>
        static void SetupServiceDependencyDataContext(DependencyObject dependencyObject, Type type)
        {
            if (dependencyObject is FrameworkElement frameworkElement
                && type != null
                && ApplicationHost.Instance.Host != null)
            {
                frameworkElement.DataContext =
                    ApplicationHost.Instance.Host.Services
                        .GetService(type);
            }
        }

        /// <summary>
        /// setup service dependency data context behavior: setup data context from dependency injector
        /// <para>wpfubehaviorsServices:ServiceDependencyDataContextBehavior.IsEnabled="True"</para>
        /// <para>resolve a view model</para>
        /// </summary>
        /// <param name="dependencyObject">dependency object</param>
        static void SetupServiceDependencyDataContext(DependencyObject dependencyObject)
        {
            if (dependencyObject is FrameworkElement frameworkElement
                && ApplicationHost.Instance.Host != null)
            {
                // {ResolveProperty}

                var resolve = GetResolve(dependencyObject);
                if (resolve != null)
                {
                    frameworkElement.DataContext = ApplicationHost.Instance.Host.Services
                        .GetService(resolve);
                    return;
                }

                // interface lookup

                var type = dependencyObject.GetType();
                var interfaceType = _servicesDependencyTypeResolver
                    .GetViewModelInterfaceType(dependencyObject.GetType());
                if (interfaceType != null)
                    frameworkElement.DataContext =
                        ApplicationHost.Instance.Host.Services
                        .GetService(interfaceType);
            }
        }

        /// <summary>
        /// trigger setup data context when attached
        /// <para>Behaviors:Interaction.Behaviors</para>
        /// </summary>
        protected override void OnAttached()
            => SetupServiceDependencyDataContext(AssociatedObject);
    }
}