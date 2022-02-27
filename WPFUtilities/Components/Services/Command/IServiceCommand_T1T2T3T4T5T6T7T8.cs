namespace WPFUtilities.Components.Services.Command
{
    /// <summary>
    /// contract of a command provided by service parameter   
    /// </summary>
    public interface IServiceCommand<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7, TParam8> : IServiceCommand
    {
        /// <summary>
        /// executes the command
        /// </summary>
        /// <param name="context">service command execute context</param>
        /// <param name="param1">TParam1 command parameter</param>
        /// <param name="param2">TParam2 command parameter</param>
        /// <param name="param3">TParam3 command parameter</param>
        /// <param name="param4">TParam4 command parameter</param>
        /// <param name="param5">TParam5 command parameter</param>
        /// <param name="param6">TParam6 command parameter</param>
        /// <param name="param7">TParam7 command parameter</param>
        /// <param name="param8">TParam8 command parameter</param>
        void Execute(IServiceCommandExecuteContext context, TParam1 param1, TParam2 param2, TParam3 param3, TParam4 param4, TParam5 param5, TParam6 param6, TParam7 param7, TParam8 param8);
    }
}
