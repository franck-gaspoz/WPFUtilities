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

        /// <summary>
        /// open a new window
        /// </summary>
        public static ICommand OpenMainWindow { get; } = OpenMainWindowCommand.Instance;


    }
}
