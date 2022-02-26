namespace WPFUtilities.Components.Services.Command
{
    /// <summary>
    /// parmeters of a IServiceCommand  
    /// </summary>
    public class ServiceCommandExecuteContext : IServiceCommandExecuteContext
    {
        /// <inheritdoc/>
        public object Caller { get; set; }

    }
}
