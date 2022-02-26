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

        /// <summary>
        /// clone this object
        /// </summary>
        /// <returns>a new instance with same properties values</returns>
        IServiceCommandExecuteContext Clone();
    }
}
