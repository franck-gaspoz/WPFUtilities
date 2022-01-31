using System;
using System.Windows.Input;

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
        protected IServiceProvider ServiceProvider;

        /// <summary>
        /// creates a new instance
        /// </summary>
        /// <param name="serviceProvider">service provider</param>
        public AbstractServiceCommand(IServiceProvider serviceProvider)
        {
            ServiceProvider = serviceProvider;
        }
    }
}
