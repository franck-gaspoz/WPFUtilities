using System.Windows.Input;

namespace WPFUtilities.Commands.Interactivity
{
    /// <summary>
    /// interactivity commands
    /// </summary>
    public static class InteractivityCommands
    {
        /// <summary>
        /// close a window
        /// </summary>
        public static ICommand CloseWindow => CloseWindowCommand.Instance;

        /// <summary>
        /// hide a window
        /// </summary>
        public static ICommand HideWindow => HideWindowCommand.Instance;

        /// <summary>
        /// open a window
        /// </summary>
        public static ICommand OpenWindow => OpenWindowCommand.Instance;
    }
}
