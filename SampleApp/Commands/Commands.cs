using System.Windows.Input;

namespace SampleApp.Commands
{
    /// <summary>
    /// commands
    /// </summary>
    public static class Commands
    {
        /// <summary>
        /// on application init callback command
        /// </summary>
        public static ICommand OnMainWindowShown { get; } = OnMainWindowShownCommand.Instance;

    }
}
