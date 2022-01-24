using System.ComponentModel;

using WPFUtilities.ComponentModels;
using WPFUtilities.Components.Component;

namespace SampleApp.Components.Logging
{
    /// <summary>
    /// log view model : exposes messages to a view, plugged with the application host loggin provider to forward log messages
    /// </summary>    
    public class LogViewModel : ModelBase, IModelBase, ILogViewModel
    {
        string _title = "";
        /// <summary>
        /// view title
        /// </summary>
        public string Title
        {
            get
            {
                return _title;
            }
            set
            {
                _title = value;
                NotifyPropertyChanged();
            }
        }

        /// <inheritdoc/>
        public BindingList<string> Messages { get; protected set; }
            = new BindingList<string>();

        public LogViewModel(IComponentHost componentHost)
        {

        }
    }
}
