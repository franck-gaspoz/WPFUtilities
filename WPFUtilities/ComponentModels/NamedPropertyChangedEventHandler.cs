using System;
using System.ComponentModel;

namespace WPFUtilities.ComponentModels
{
    /// <summary>
    /// listen for property name changes and triggers action handler
    /// </summary>
    public class NamedPropertyChangedEventHandler
    {
        readonly string _PropertyName;
        readonly IModelBase _notifiableModel;
        readonly Action<object, PropertyChangedEventArgs> _handlerAction;

        /// <summary>
        /// listen for property name changes and triggers action handler
        /// </summary>
        /// <param name="propertyName">property name</param>
        /// <param name="notifiableModel">notifiable action</param>
        /// <param name="handlerAction">handler action</param>
        public NamedPropertyChangedEventHandler(
            string propertyName,

            IModelBase notifiableModel,
            Action<object, PropertyChangedEventArgs> handlerAction
            )
        {
            _PropertyName = propertyName;
            _notifiableModel = notifiableModel;
            _handlerAction = handlerAction;
            _notifiableModel.PropertyChanged += (o, e) =>
            {
                if (e.PropertyName == _PropertyName)
                    _handlerAction?.Invoke(o, e);
            };
        }
    }
}
