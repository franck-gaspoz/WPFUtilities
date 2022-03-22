using System;
using System.ComponentModel;

using WPFUtilities.ComponentModels;

namespace WPFUtilities.Extensions.Models
{
    /// <summary>
    /// models extensions
    /// </summary>
    public static class ModelExtensions
    {
        /// <summary>
        /// on property having name changed event handler
        /// </summary>
        /// <typeparam name="ModelType"></typeparam>
        /// <param name="model">model</param>
        /// <param name="propertyName">property name</param>
        /// <param name="handlerAction">handler</param>
        public static void OnPropertyChanged<ModelType>(
            this IModelBase model,
            string propertyName,
            Action<ModelType, PropertyChangedEventArgs> handlerAction
            )
        {
            model.PropertyChanged += (o, e) =>
            {
                if (e.PropertyName == propertyName)
                    handlerAction.Invoke((ModelType)model, e);
            };
        }
    }
}
