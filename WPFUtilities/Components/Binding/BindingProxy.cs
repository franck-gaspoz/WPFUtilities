using System.Windows;

namespace WPFUtilities.Components.Bindings
{
    /// <summary>
    /// binding proxy
    /// </summary>
    public class BindingProxy : Freezable
    {
        #region Overrides of Freezable

        /// <inheritdoc/>
        protected override Freezable CreateInstanceCore()
        {
            return new BindingProxy();
        }

        #endregion

        /// <summary>
        /// Data
        /// </summary>
        public object Data
        {
            get { return (object)GetValue(DataProperty); }
            set { SetValue(DataProperty, value); }
        }

        /// <summary>
        /// data property
        /// </summary>
        public static readonly DependencyProperty DataProperty =
            DependencyProperty.Register(
                "Data",
                typeof(object),
                typeof(BindingProxy),
                new UIPropertyMetadata(null));
    }
}
