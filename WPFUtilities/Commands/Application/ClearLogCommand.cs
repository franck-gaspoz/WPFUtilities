using System.Windows.Input;

using WPFUtilities.Components.Logging.ListLogger;

namespace WPFUtilities.Commands.Application
{
    /// <summary>
    /// clear log
    /// <para>use application logging services</para>
    /// </summary>
    public class ClearLogCommand : AbstractCommand<LogCommand>, ICommand
    {
        IListLoggerModel _logModel;

        /// <summary>
        /// creates a new instance
        /// </summary>
        /// <param name="logModel">list logger model</param>
        public ClearLogCommand(IListLoggerModel logModel)
        {
            _logModel = logModel;
        }

        /// <inheritdoc/>
        public override bool CanExecute(object parameter)
            => true;

        /// <summary>
        /// clear log command
        /// </summary>
        /// <param name="parameter">not used</param>
        public override void Execute(object parameter)
        {
            _logModel.LogItems.Clear();
        }
    }
}