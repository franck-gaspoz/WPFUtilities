using System;
using System.Collections.Generic;
using System.Windows;

using WPFUtilities.Extensions.DependencyObjects;

namespace WPFUtilities.Components.UI
{
    /// <summary>
    /// framework element extensions
    /// </summary>
    public static partial class Data
    {
        /// <summary>
        /// get brush
        /// </summary>
        /// <param name="dependencyObject">dependency object</param>
        /// <returns>value</returns>
        public static Dictionary<string, object> GetAdditionalData(DependencyObject dependencyObject) => (Dictionary<string, object>)dependencyObject.GetValue(AdditionalDataProperty);

        /// <summary>
        /// set brush
        /// </summary>
        /// <param name="dependencyObject">dependency Object</param>
        /// <param name="AdditionalData">AdditionalData</param>
        public static void SetAdditionalData(DependencyObject dependencyObject, Dictionary<string, object> AdditionalData) => dependencyObject.SetValue(AdditionalDataProperty, AdditionalData);

        /// <summary>
        /// margin property
        /// </summary>
        public static readonly DependencyProperty AdditionalDataProperty =
            DependencyObjectExtensions.RegisterAttached(
                "AdditionalData",
                typeof(Dictionary<string, object>),
                typeof(Grids),
                new UIPropertyMetadata(null));

        /// <summary>
        /// add or remplace additional data of a dependency object (property AdditionalDataProperty)
        /// </summary>
        /// <param name="dependencyObject">target</param>
        /// <param name="key">key</param>
        /// <param name="value">value</param>
        public static void AddOrRemplaceAdditionalData(DependencyObject dependencyObject, string key, object value)
        {
            var dico = (Dictionary<string, object>)dependencyObject.GetValue(AdditionalDataProperty);
            if (dico == null)
            {
                dico = new Dictionary<string, object>();
                dependencyObject.SetValue(AdditionalDataProperty, dico);
            }
            if (dico.ContainsKey(key))
                dico[key] = value;
            else
                dico.Add(key, value);
        }

        /// <summary>
        /// indicates if a dependency object has an additional data with given key
        /// </summary>
        /// <param name="dependencyObject">dependency object</param>
        /// <param name="key">key</param>
        /// <returns>true if additional data exists, false otherwise</returns>
        public static bool HasAdditionalData(DependencyObject dependencyObject, string key)
        {
            var dico = (Dictionary<string, object>)dependencyObject.GetValue(AdditionalDataProperty);
            return (dico != null && dico.ContainsKey(key));
        }

        /// <summary>
        /// return additional data having key from dependency object
        /// </summary>
        /// <typeparam name="T">expectged additional data value type</typeparam>
        /// <param name="dependencyObject">dependency object</param>
        /// <param name="key">key</param>
        /// <returns>additional data object value</returns>
        /// <exception cref="InvalidOperationException">dependency object has no additional data or no one with the specified key</exception>
        public static T GetAdditionalData<T>(DependencyObject dependencyObject, string key)
        {
            if (!HasAdditionalData(dependencyObject, key)) throw new InvalidOperationException($"additional data with key '{key}' not found");
            return (T)((Dictionary<string, object>)dependencyObject.GetValue(AdditionalDataProperty))[key];
        }
    }
}
