using GIS.Common.FileUtilities;

namespace WPFUtilities.Command.Interactivity
{
    public class OpenFolderCommand : CommandBase<string>
    {
        public override void Execute(object parameter)
        {
            var p = Cast(parameter);
            FileUtilities
                .GetInstance()
                .OpenFolderShell(p);
        }
    }
}
