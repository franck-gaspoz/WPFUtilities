
using System.Reflection;

using WPFUtilities.Components.UI.KeyValueDataGridControl;

namespace SampleApp.Components.ComponentHosts.Common
{
    /// <summary>
    /// key value item view model extension
    /// </summary>
    public class ExtendedKeyValueItemViewModel
        : KeyValueItemViewModel
    {
        string _groupName = null;
        /// <summary>
        /// groupe name
        /// </summary>
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
        /// <summary>
        /// assembly
        /// </summary>
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
