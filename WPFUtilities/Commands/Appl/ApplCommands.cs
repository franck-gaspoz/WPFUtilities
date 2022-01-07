using System.Windows.Input;

namespace WPFUtilities.Commands.Appl
{
    /// <summary>
    /// application commands
    /// </summary>
    public static class ApplCommands
    {
        /// <summary>
        /// close application
        /// </summary>
        public static ICommand ApplicationClose => CloseApplicationCommand.Instance;

        /// <summary>
        /// log a string or an object ToString (level Information)
        /// </summary>
        public static ICommand Log { get; } = LogCommand.Instance;

    }
}
