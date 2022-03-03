using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace WPFUtilities.ComponentModels
{
    /// <summary>
    /// model base interface
    /// </summary>
    public interface IModelBase
    {
        /// <summary>
        /// enable/disable data validation
        /// </summary>
        bool IsDataValidationEnabled { get; set; }

        /// <summary>
        /// property changed event
        /// </summary>
        event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// indicates if any data has changed in the model
        /// </summary>
        bool HasModelChanged { get; }

        /// <summary>
        /// reset model state
        /// </summary>
        void ResetModelState();

        /// <summary>
        /// notify a property has changed
        /// </summary>
        /// <param name="propertyName">property name (caller member name if omitted)</param>
        void NotifyPropertyChanged([CallerMemberName] string propertyName = "");
    }
}
