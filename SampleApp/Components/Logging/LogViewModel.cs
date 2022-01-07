using System.ComponentModel;

using Microsoft.Extensions.Logging;

using WPFUtilities.Attributes;
using WPFUtilities.ComponentModels;

namespace SampleApp.Components.Logging
{
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
            logger.LogDebug("LogViewModel created");
        }
    }
}
