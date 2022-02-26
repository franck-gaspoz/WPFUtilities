
using WPFUtilities.Components.ServiceComponent;
using WPFUtilities.Components.Services.Command;

namespace WPFUtilities.Commands.Abstract
{
    /// <summary>
    /// command that requires service context
    /// </summary>
    public abstract class AbstractServiceCommand<ImplType, TParam> :
        AbstractServiceCommand<ImplType>,
        IServiceCommand<TParam>
        where ImplType : IServiceCommand<TParam>
    {
        /// <summary>
        /// creates a new instance
        /// </summary>
        /// <param name="serviceProvider">service component provider</param>
        public AbstractServiceCommand(IServiceComponentProvider serviceProvider)
            : base(serviceProvider) { }

        /// <inheritdoc/>
        public override void Execute(object parameter, IServiceCommandExecuteContext context)
            => Execute(CastParameterTo<TParam>(0, parameter), context);

        /// <inheritdoc/>
        public abstract void Execute(TParam parameter, IServiceCommandExecuteContext context);
    }
}
