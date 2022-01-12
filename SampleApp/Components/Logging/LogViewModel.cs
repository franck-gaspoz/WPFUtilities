using System.ComponentModel;

using WPFUtilities.ComponentModels;

namespace SampleApp.Components.Logging
{
    /// <summary>
    /// log view model : exposes messages to a view, plugged with the application host loggin provider to forward log messages
    /// </summary>    
    public class LogViewModel : ModelBase, IModelBase, ILogViewModel
    {
        /// <inheritdoc/>
        public BindingList<string> Messages { get; protected set; }
            = new BindingList<string>();

        public LogViewModel()
        {

        }
    }
}
