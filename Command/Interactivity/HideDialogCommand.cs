using System;
using System.Windows;

namespace WPFUtilities.Command.Interactivity
{
    public class HideDialogCommand
        : CommandBase<Func<Window>>
    {
        public override bool CanExecute(object parameter)
        {
            return true;
        }

        public override void Execute(object parameter)
        {
            if (parameter is Window w
                && w!=null)
                w.Hide();
            else
            {
                var a = Cast(parameter);
                var win = a?.Invoke();
                win.Hide();
            }
        }
    }
}
