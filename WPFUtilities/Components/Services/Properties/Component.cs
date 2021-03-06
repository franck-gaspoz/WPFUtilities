using System;
using System.Windows;

using WPFUtilities.Components.ServiceComponent;
using WPFUtilities.Extensions.DependencyObjects;

namespace WPFUtilities.Components.Services.Properties
{
    /// <summary>
    /// component type value dependency property
    /// </summary>
    public static class Component
    {
        #region component host

        /// <summary>
        /// get component host
        /// </summary>
        /// <param name="obj">dependency object</param>
        /// <returns>component host</returns>
        public static IComponentHost GetComponentHost(DependencyObject obj)
        {
            return (IComponentHost)obj.GetValue(ComponentHostProperty);
        }

        /// <summary>
        /// set component host
        /// </summary>
        /// <param name="obj">dependency object</param>
        /// <param name="value">component host</param>
        public static void SetComponentHost(DependencyObject obj, IComponentHost value)
        {
            obj.SetValue(ComponentHostProperty, value);
        }

        /// <summary>
        /// component host property
        /// </summary>
        public static readonly DependencyProperty ComponentHostProperty =
            DependencyObjectExtensions.RegisterAttached(
                "ComponentHost",
                typeof(IComponentHost),
                typeof(Component),
                new PropertyMetadata(null));

        #endregion

        #region component type

        /// <summary>
        /// get component type
        /// </summary>
        /// <param name="obj">dependency object</param>
        /// <returns>component type</returns>
        public static Type GetType(DependencyObject obj)
        {
            return (Type)obj.GetValue(TypeProperty);
        }

        /// <summary>
        /// set component type
        /// </summary>
        /// <param name="obj">dependency object</param>
        /// <param name="value">component type</param>
        public static void SetType(DependencyObject obj, Type value)
        {
            obj.SetValue(TypeProperty, value);
        }

        /// <summary>
        /// component property
        /// </summary>
        public static readonly DependencyProperty TypeProperty =
            DependencyObjectExtensions.RegisterAttached(
                "Type",
                typeof(Type),
                typeof(Component),
                new PropertyMetadata(null));

        #endregion
    }
}
