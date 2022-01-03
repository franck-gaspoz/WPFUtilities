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

    }
}
