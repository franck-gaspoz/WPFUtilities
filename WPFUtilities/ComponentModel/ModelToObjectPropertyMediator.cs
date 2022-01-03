using System.ComponentModel;

namespace WPFUtilities.ComponentModel
{
    /// <summary>
    /// a mediator that transfer a model property value to an object property value. if property name is null select any property
    /// </summary>
    public class ModelToObjectPropertyMediator : ModelToObjectMediator
    {
        /// <summary>
        /// property name
        /// </summary>
        string _propertyName;

        /// <summary>
        /// build a model to object mediator for a source model and a target object, for a property. binds the mediator to the source
        /// </summary>
        /// <param name="source">mediator source</param>
        /// <param name="target">mediator target</param>
        /// <param name="propertyName">property name</param>
        public void InitializeMediate(IModelBase source, object target, string propertyName)
        {
            _propertyName = propertyName;
            base.InitializeMediate(source, target);
        }

        /// <summary>
        /// clear the mediator bindings
        /// </summary>
        public override void ClearMediate()
        {
            _propertyName = null;
            base.ClearMediate();
        }

        /// <summary>
        /// mediator source property changed event handler
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="eventArgs">event args</param>
        protected override void ModelBase_PropertyChanged(object sender, PropertyChangedEventArgs eventArgs)
        {
            if (_propertyName == null || eventArgs.PropertyName != _propertyName) return;
            base.ModelBase_PropertyChanged(sender, eventArgs);
        }
    }
}
