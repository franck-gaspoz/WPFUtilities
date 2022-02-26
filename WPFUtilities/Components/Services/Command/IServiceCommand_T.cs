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
        /// <param name="context">service command execute context</param>
        /// <param name="parameter">TParam command parameter</param>
        void Execute(IServiceCommandExecuteContext context, TParam parameter);
    }
}
