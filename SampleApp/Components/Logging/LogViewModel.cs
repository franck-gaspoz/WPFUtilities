using System.ComponentModel;

using Microsoft.Extensions.Logging;

using WPFUtilities.Attributes;
using WPFUtilities.ComponentModels;

namespace SampleApp.Components.Logging
{
    /// <summary>
    /// log view model : exposes messages to a view, plugged with the application host loggin provider to forward log messages
    /// </summary>
    [DependencyService]
    public class LogViewModel : ModelBase, IModelBase, ILogViewModel
    {
        /// <inheritdoc/>
        public BindingList<string> Messages { get; protected set; }
            = new BindingList<string>();

        /// <summary>
        /// creates a new instance
        /// </summary>
        public LogViewModel(ILogger<LogViewModel> logger)
        {
            logger.LogDebug($"{this.GetType().Name} created");
        }
    }
}
