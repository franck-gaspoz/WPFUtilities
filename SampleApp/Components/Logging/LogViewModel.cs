using System.ComponentModel;
using System.Linq;

using Microsoft.Extensions.Logging;

using WPFUtilities.ComponentModels;
using WPFUtilities.Components.Component;
using WPFUtilities.Components.Logging.ListLogger;

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

        public LogViewModel(IComponentHost componentHost)
        {
            // need to get the list logger configuration
            var loggerProvider = componentHost.Services.GetServices<ILoggerProvider>()
                .OfType<ListLoggerProvider>()
                .FirstOrDefault();

            var listLoggerModel1 = componentHost.Services.GetService<ListLoggerModel>();
            Messages = new BindingList<string>(listLoggerModel1.LogItems.ToList());

            if (loggerProvider != null)
            {
                var loggerConfig = loggerProvider.GetCurrentConfig();
                loggerConfig.Targets.Add(this.Messages);
            }
        }
    }
}
