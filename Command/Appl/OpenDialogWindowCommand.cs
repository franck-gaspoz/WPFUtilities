using System;
using System.Linq;
using System.Reflection;
using System.Windows;

namespace WPFUtilities.Command.Appl
{
    public class OpenDialogWindowCommand
        : CommandBase<string>
    {
        public OpenDialogWindowCommand()
        {
        }

        public override bool CanExecute(object parameter)
        {
            return Application.Current != null && Application.Current.MainWindow != null;
        }

        public override void Execute(object parameter)
        {
            Type t = null;
            if (parameter is string name)
            {
                t = Assembly
                    .GetEntryAssembly()
                    .DefinedTypes
                    .Where(x => x.Name == (string)parameter)
                    .FirstOrDefault();
            }

            if (parameter is Type ty)
                t = ty;

            if (t != null)
            {
                var w = (Window)Activator.CreateInstance(t);
                var r = w.ShowDialog();
            }
        }
    }
}
