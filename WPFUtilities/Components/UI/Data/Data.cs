using System.Collections.Generic;
using System.Windows;

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
            DependencyProperty.RegisterAttached(
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
    }
}
