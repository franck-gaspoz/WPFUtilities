using System.Windows.Input;

using Microsoft.Extensions.Logging;

using WPFUtilities.Commands.Abstract;

namespace SampleApp.Components.Main
{
    /// <summary>
    /// on application init command callback
    /// </summary>
    public class OnMainWindowShownCommand : AbstractCommand<OnMainWindowShownCommand>, ICommand
    {
        ILogger<OnMainWindowShownCommand> _logger;

        /// <summary>
        /// creates a new instance
        /// </summary>
        /// <param name="logger">logger</param>
        public OnMainWindowShownCommand(ILogger<OnMainWindowShownCommand> logger)
        {
            _logger = logger;
        }

        /// <inheritdoc/>
        public override bool CanExecute(object parameter)
            => true;

        /// <summary>
        /// on application init command callback
        /// </summary>
        /// <param name="parameter">not used</param>
        public override void Execute(object parameter)
            => _logger.LogInformation("* main window is shown *");
    }
}