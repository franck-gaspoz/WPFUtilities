using System;
using System.ComponentModel;
using System.Windows;

namespace WPFUtilities.Extensions.DependencyObjects
{
    /// <summary>
    /// dependency objects extensions
    /// </summary>
    public static class DependencyObjectExtensions
    {
        /// <summary>
        /// add a new or get an existing dependency property value
        /// </summary>
        /// <typeparam name="T">expected value type of the property</typeparam>
        /// <param name="dependencyObject">dependency object</param>
        /// <param name="property">property</param>
        /// <returns>value of T</returns>
        public static T AddOrGetValue<T>(
            this DependencyObject dependencyObject,
            DependencyProperty property)
            where T : new()
        {
            var value = dependencyObject.GetValue<T>(property);
            if (value == null)
            {
                value = new T();
                dependencyObject.SetValue<T>(property, value);
            }
            return value;
        }

        /// <summary>
        /// sets the value of a dependency project from a value of a given type
        /// </summary>
        /// <typeparam name="T">value type</typeparam>
        /// <param name="dependencyObject">dependency object</param>
        /// <param name="property">property</param>
        /// <param name="value">value</param>
        public static void SetValue<T>(
            this DependencyObject dependencyObject,
            DependencyProperty property,
            T value)
            => dependencyObject.SetValue(property, value);

        /// <summary>
        /// returns value of dependency property casted to T
        /// </summary>
        /// <typeparam name="T">expected value type</typeparam>
        /// <param name="dependencyObject">dependency object</param>
        /// <param name="property">property</param>
        /// <returns>property value of T</returns>
        public static T GetValue<T>(
            this DependencyObject dependencyObject,
            DependencyProperty property
            )
            => (T)dependencyObject.GetValue(property);

        /// <summary>
        /// triggers an an action when the value of a dependency property change
        /// </summary>
        /// <typeparam name="T">source type</typeparam>
        /// <param name="dependencyObject">dependency object (type T)</param>
        /// <param name="property">dependency property</param>
        /// <param name="action">action</param>
        /// <param name="repeat"></param>
        public static void OnValueChanged<T>(
            this T dependencyObject,
            DependencyProperty property,
            Action<EventArgs> action,
            bool repeat = true
            )
            where T : DependencyObject
        {
            void ValueChanged(object sender, EventArgs args)
            {
                if (!repeat)
                    DependencyPropertyDescriptor.
                        FromProperty(property, typeof(T))
                        .RemoveValueChanged(
                            dependencyObject,
                            ValueChanged
                        );
                action(args);
            }

            DependencyPropertyDescriptor.
                FromProperty(property, typeof(T))
                .AddValueChanged(
                    dependencyObject,
                    ValueChanged
                );
        }

        static T Try<T>(Func<T> fun)
        {
            try
            {
                return fun();
            }
            catch (Exception ex)
            {
#if DEBUG
                System.Diagnostics.Debug.WriteLine(ex);
#endif
                return default(T);
            }
        }

        /// <summary>
        /// fallback dependency property
        /// </summary>
        public static readonly DependencyProperty FallBackProperty =
            DependencyObjectExtensions.Register(
                "FallBackProperty",
                typeof(object),
                typeof(DependencyObject),
                new UIPropertyMetadata(null));

        static DependencyProperty Try(Func<DependencyProperty> fun)
            => Try<DependencyProperty>(fun)
                ?? FallBackProperty;

        /// <summary>
        /// register a dependency property
        /// </summary>
        /// <param name="name">name</param>
        /// <param name="propertyType">property type</param>
        /// <param name="ownerType">owner type</param>
        /// <returns>dependecy property or null if fail</returns>
        public static DependencyProperty Register(
            string name,
            Type propertyType,
            Type ownerType)
                => Try(() => DependencyProperty
                    .Register(name, propertyType, ownerType));

        /// <summary>
        /// register a dependency property
        /// </summary>
        /// <param name="name">name</param>
        /// <param name="propertyType">property type</param>
        /// <param name="ownerType">owner type</param>
        /// <param name="typeMetadata">type meta data</param>
        /// <returns>dependecy property or null if fail</returns>
        public static DependencyProperty Register(
            string name,
            Type propertyType,
            Type ownerType,
            PropertyMetadata typeMetadata)
                => Try(() => DependencyProperty
                    .Register(name, propertyType, ownerType, typeMetadata));

        /// <summary>
        /// register a dependency property
        /// </summary>
        /// <param name="name">name</param>
        /// <param name="propertyType">property type</param>
        /// <param name="ownerType">owner type</param>
        /// <param name="typeMetadata">type meta data</param>
        /// <param name="validateValueCallback">validate value callback</param>
        /// <returns>dependecy property or null if fail</returns>
        public static DependencyProperty Register(
            string name,
            Type propertyType,
            Type ownerType,
            PropertyMetadata typeMetadata,
            ValidateValueCallback validateValueCallback)
                => Try(() => DependencyProperty
                    .Register(name, propertyType, ownerType, typeMetadata, validateValueCallback));

        /// <summary>
        /// registered attached dependency property
        /// </summary>
        /// <param name="name">name</param>
        /// <param name="propertyType">property type</param>
        /// <param name="ownerType">owner type</param>
        /// <returns>dependecy property or null if fail</returns>
        public static DependencyProperty RegisterAttached(
            string name,
            Type propertyType,
            Type ownerType)
                => Try(() => DependencyProperty.
                    RegisterAttached(
                        name, propertyType, ownerType
                        ));

        /// <summary>
        /// registered attached dependency property
        /// </summary>
        /// <param name="name">name</param>
        /// <param name="propertyType">property type</param>
        /// <param name="ownerType">owner type</param>
        /// <param name="defaultMetadata">default meta data</param>
        /// <returns>dependecy property or null if fail</returns>
        public static DependencyProperty RegisterAttached(
            string name,
            Type propertyType,
            Type ownerType,
            PropertyMetadata defaultMetadata)
                => Try(() => DependencyProperty.
                    RegisterAttached(
                        name, propertyType, ownerType, defaultMetadata
                        ));

        /// <summary>
        /// registered attached dependency property
        /// </summary>
        /// <param name="name">name</param>
        /// <param name="propertyType">property type</param>
        /// <param name="ownerType">owner type</param>
        /// <param name="defaultMetadata">default meta data</param>
        /// <param name="validateValueCallback">validate value callback</param>
        /// <returns>dependecy property or null if fail</returns>
        public static DependencyProperty RegisterAttached(
            string name,
            Type propertyType,
            Type ownerType,
            PropertyMetadata defaultMetadata,
            ValidateValueCallback validateValueCallback)
                => Try(() => DependencyProperty.
                    RegisterAttached(
                        name, propertyType, ownerType, defaultMetadata, validateValueCallback
                        ));
    }
}
