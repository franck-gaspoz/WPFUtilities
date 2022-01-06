using System.Windows.Input;

namespace WPFUtilities.Commands.Render
{
    /// <summary>
    /// display commands
    /// </summary>
    public static class DisplayCommands
    {
        /// <summary>
        /// zoom an element
        /// </summary>
        public static ICommand Zoom => ZoomCommand.Instance;
    }
}
