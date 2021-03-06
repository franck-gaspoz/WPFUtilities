namespace WPFUtilities.Components.Services.Command
{
    /// <summary>
    /// contract of a command provided by service parameter   
    /// </summary>
    public interface IServiceCommand<TParam1, TParam2, TParam3> : IServiceCommand
    {
        /// <summary>
        /// executes the command
        /// </summary>
        /// <param name="context">service command execute context</param>
        /// <param name="param1">TParam1 command parameter</param>
        /// <param name="param2">TParam2 command parameter</param>
        /// <param name="param3">TParam3 command parameter</param>
        void Execute(IServiceCommandExecuteContext context, TParam1 param1, TParam2 param2, TParam3 param3);
    }
}
