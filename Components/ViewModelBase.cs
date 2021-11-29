using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;

namespace WPFUtilities.Components
{
    public class ViewModelBase :
        Dispatchable
        , INotifyPropertyChanged
        , INotifyDataErrorInfo
    {
        bool _IsDirty = false;
        public bool IsDirty
        {
            get
            {
                return _IsDirty;
            }
            set
            {
                _IsDirty = value;
                NotifyPropertyChanged();
                NotifyPropertyChanged(nameof(IsCancelable));
            }
        }

        public bool IsChangesTrackingEnabled = false;

        public bool IsCancelable
        {
            get
            {
                return _IsDirty && _isCancelEnabled;
            }
            set
            {
                NotifyPropertyChanged();
            }
        }

        bool _isCancelEnabled = true;
        public bool IsCancelEnabled
        {
            get
            {
                return _isCancelEnabled;
            }
            set
            {
                _isCancelEnabled = value;
                NotifyPropertyChanged();
                NotifyPropertyChanged(nameof(IsCancelable));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;

        protected Dictionary<string, List<string>> errors = new Dictionary<string, List<string>>();

        public string ErrorResume
        {
            get
            {
                return HasErrors ? errors.First().Value[0] : "";
            }
        }
        public bool HasErrors => errors.Count > 0;

        public void NotifyPropertyChanged([CallerMemberName] string propName = "")
        {
            if (IsChangesTrackingEnabled && !IsDirty)
                IsDirty = true;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }

        protected void SetErrors(List<string> propertyErrors, [CallerMemberName] string propertyName = "")
        {
            // Clear any errors that already exist for this property.
            errors.Remove(propertyName);
            // Add the list collection for the specified property.
            errors.Add(propertyName, propertyErrors);
            // Raise the error-notification event.
            ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(propertyName));
        }

        protected void ClearErrors([CallerMemberName] string propertyName = "")
        {
            // Remove the error list for this property.
            errors.Remove(propertyName);
            // Raise the error-notification event.
            ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(propertyName));
        }

        public IEnumerable GetErrors(string propertyName)
        {
            if (string.IsNullOrEmpty(propertyName))
            {
                // Provide all the error collections.
                return (errors.Values);
            }
            else
            {
                // Provide the error collection for the requested property if it has errors
                if (errors.ContainsKey(propertyName))
                {
                    return (errors[propertyName]);
                }
                else
                {
                    return null;
                }
            }
        }

        public ViewModelBase()
        {
        }

        public R Run<R>(Func<R> func)
        {
            try
            {
                return func();
            }
            catch (Exception ex)
            {
                var m = ex + "";
                MessageBox.Show(m, "error", MessageBoxButton.OK, MessageBoxImage.Error);
                return default;
            }
        }

        public void Run(Action action)
            => Run<object>(() => { action(); return null; });
    }
}
