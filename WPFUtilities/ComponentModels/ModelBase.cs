using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace WPFUtilities.ComponentModels
{
    /// <summary>
    /// base model with notifiable and validable data
    /// </summary>
    public class ModelBase :
        ValidableModel,
        IModelBase,
        INotifyPropertyChanged
    {
        /// <inheritdoc/>
        public bool IsDataValidationEnabled { get; set; } = true;

        bool _hasModelChanged = false;
        /// <inheritdoc/>
        public bool HasModelChanged
        {
            get => _hasModelChanged;

            set
            {
                _hasModelChanged = value;
                NotifyPropertyChanged();
            }
        }

        /// <inheritdoc/>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// build a new model base
        /// </summary>
        public ModelBase()
        {
            ErrorsChanged += ModelBase_ErrorsChanged;
        }

        /// <summary>
        /// errors changed event handler
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">event args</param>
        private void ModelBase_ErrorsChanged(object sender, DataErrorsChangedEventArgs e)
        {
            NotifyPropertyChanged(nameof(IsValid));
        }

        /// <inheritdoc/>
        public void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            if (!HasModelChanged) HasModelChanged = true;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            if (IsDataValidationEnabled && propertyName != nameof(IsValid))
            {
                Validate(propertyName);
            }
        }

        /// <inheritdoc/>
        public void ResetModelState() => HasModelChanged = false;
    }
}

