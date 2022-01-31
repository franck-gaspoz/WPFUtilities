using System.Windows.Input;

using WPFUtilities.Components.ServiceComponent;

namespace WPFUtilities.Commands.Abstract
{
    /// <summary>
    /// command that requires service context
    /// </summary>
    public abstract class AbstractServiceCommand<ImplType> :
        AbstractCommand<ImplType>,
        ICommand
        where ImplType : ICommand
    {
        /// <summary>
        /// service provider
        /// </summary>
        protected IServiceComponentProvider ServiceProvider;

        /// <summary>
        /// creates a new instance
        /// </summary>
        /// <param name="serviceProvider">service component provider</param>
        public AbstractServiceCommand(IServiceComponentProvider serviceProvider)
        {
            ServiceProvider = serviceProvider;
        }
    }
}
