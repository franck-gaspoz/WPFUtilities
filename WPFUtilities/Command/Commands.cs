using WPFUtilities.Command.Appl;
using WPFUtilities.Command.Display;
using WPFUtilities.Command.Interactivity;
using System.Windows.Input;

namespace WPFUtilities.Command
{
    public static class Commands
    {
        public static ICommand ApplicationClose { get; } = new ApplicationCloseCommand();

        public static ICommand OpenDialogWindow { get; } = new OpenDialogWindowCommand();

        public static ICommand Zoom { get; } = new ZoomCommand();

        public static ICommand OkDialog { get; } = new OkDialogCommand();

        public static ICommand HideDialog { get; } = new HideDialogCommand();

        public static ICommand CancelDialog { get; } = new CancelDialogCommand();

    }
}
 