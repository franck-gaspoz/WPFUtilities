
using System.Reflection;

using SampleApp.Components.Data.KeyValue;

namespace SampleApp.Components.ComponentHosts.Common
{
    /// <summary>
    /// key value item view model extension
    /// </summary>
    public class ExtendedKeyValueItemViewModel
        : KeyValueItemViewModel
    {
        string _groupName = null;
        /// <inheritdoc/>
        public string GroupName
        {
            get
            {
                return _groupName;
            }
            set
            {
                _groupName = value;
                NotifyPropertyChanged();
            }
        }

        Assembly _assembly = null;
        /// <inheritdoc/>
        public Assembly Assembly
        {
            get
            {
                return _assembly;
            }
            set
            {
                _assembly = value;
                NotifyPropertyChanged();
            }
        }
    }
}
