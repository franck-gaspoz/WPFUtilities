using System.Windows.Input;

using Microsoft.Extensions.Logging;

namespace WPFUtilities.Commands.Application
{
    /// <summary>
    /// log a string or an object ToString (level Information)
    /// </summary>
    public class LogCommand : AbstractCommand<LogCommand>, ICommand
    {
        ILogger<LogCommand> _logger;

        /// <summary>
        /// creates a new instance
        /// </summary>
        /// <param name="logger">logger</param>
        public LogCommand(ILogger<LogCommand> logger)
        {
            _logger = logger;
        }

        /// <inheritdoc/>
        public override bool CanExecute(object parameter)
            => true;

        /// <summary>
        /// log command
        /// </summary>
        /// <param name="parameter">a string to be logged</param>
        public override void Execute(object parameter)
        {
            if (parameter is string str)
                _logger.LogInformation(str);
            else
                _logger.LogInformation(parameter?.ToString());
        }
    }
}