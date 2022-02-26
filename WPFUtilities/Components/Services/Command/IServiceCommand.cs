using System.Windows.Input;

namespace WPFUtilities.Components.Services.Command
{
    /// <summary>
    /// contract of a command provided by service parameter   
    /// </summary>
    public interface IServiceCommand : ICommand
    {
        /// <summary>
        /// executes the command
        /// </summary>
        /// <param name="parameter">command parameter</param>
        /// <param name="context">service command execute context</param>
        void Execute(object parameter, IServiceCommandExecuteContext context);
    }
}
