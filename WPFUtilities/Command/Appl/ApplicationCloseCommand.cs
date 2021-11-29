using System.Windows;

namespace WPFUtilities.Command.Appl
{
    public class ApplicationCloseCommand 
        : CommandBase<CommandParametersBase>
    {
        public ApplicationCloseCommand()
        {
        }
        
        public override bool CanExecute(object parameter)
        {
            return Application.Current != null && Application.Current.MainWindow != null;
        }

        public override void Execute(object parameter)
        {
            Application.Current.MainWindow.Close();
        }
    }
}
