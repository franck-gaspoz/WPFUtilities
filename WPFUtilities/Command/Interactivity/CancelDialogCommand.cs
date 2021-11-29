using System;
using System.Windows;

namespace WPFUtilities.Command.Interactivity
{
    public class CancelDialogCommand
        : CommandBase<Func<Window>>
    {
        public override bool CanExecute(object parameter)
        {
            return true;
        }

        public override void Execute(object parameter)
        {
            if (parameter is Window win
                && win!=null)
                win.Close();
            else
            {
                var a = Cast(parameter);
                var w = a?.Invoke();
                w.Close();
            }
        }
    }
}