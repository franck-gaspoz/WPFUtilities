using System;
using System.Windows.Input;

using WPFUtilities.Components.ServiceComponent;
using WPFUtilities.Components.Services.Command;

namespace WPFUtilities.Commands.Abstract
{
    /// <summary>
    /// command that requires service context
    /// </summary>
    public abstract class AbstractServiceCommand<ImplType> :
        AbstractCommand<ImplType>,
        ICommand
        where ImplType : IServiceCommand
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

        /// <inheritdoc/>
        public override void Execute(object parameter)
            => throw new InvalidOperationException("context is required. use Execute(object parameter, IServiceCommandExecuteContext context) instead");

        /// <summary>
        /// execute the command
        /// </summary>
        /// <param name="parameter">parameter</param>
        /// <param name="context">execute context</param>
        public abstract void Execute(object parameter, IServiceCommandExecuteContext context);
    }
}
