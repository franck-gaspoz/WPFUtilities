namespace WPFUtilities.Components.Services.Command
{
    /// <summary>
    /// context of an execute of a IServiceCommand  
    /// </summary>
    public interface IServiceCommandExecuteContext
    {
        /// <summary>
        /// the command calller
        /// </summary>
        object Caller { get; set; }
    }
}
