using System.Windows.Input;

namespace WPFUtilities.Commands.Application
{
    /// <summary>
    /// application commands (application scope: created by application host)
    /// <para>application scope commands can be referenced directly thought their static instance (SingletonService)</para>
    /// </summary>
    public static class ApplicationCommands
    {
        /// <summary>
        /// close application
        /// </summary>
        public static ICommand ApplicationClose => CloseApplicationCommand.Instance;

        /// <summary>
        /// log a string or an object ToString (at level Information)
        /// </summary>
        public static ICommand Log { get; } = LogCommand.Instance;

        /// <summary>
        /// clear log
        /// </summary>
        public static ICommand ClearLog { get; } = ClearLogCommand.Instance;

        /// <summary>
        /// open a new main window from application base settings
        /// </summary>
        public static ICommand OpenMainWindow { get; } = OpenMainWindowCommand.Instance;

        /// <summary>
        /// close a window
        /// </summary>
        public static ICommand CloseWindow { get; } = CloseWindowCommand.Instance;
    }
}
