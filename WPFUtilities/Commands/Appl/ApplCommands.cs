using System.Windows.Input;

namespace WPFUtilities.Commands.Appl
{
    /// <summary>
    /// application commands (application scope: created by application host)
    /// <para>application scope commands can be referenced directly thought their static instance (SingletonService)</para>
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
