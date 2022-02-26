namespace WPFUtilities.Components.Services.Command
{
    /// <summary>
    /// contract of a command provided by service parameter   
    /// </summary>
    public interface IServiceCommand<TParam> : IServiceCommand
    {
        /// <summary>
        /// executes the command
        /// </summary>
        /// <param name="parameter">TParam command parameter</param>
        /// <param name="context">service command execute context</param>
        void Execute(TParam parameter, IServiceCommandExecuteContext context);
    }
}
