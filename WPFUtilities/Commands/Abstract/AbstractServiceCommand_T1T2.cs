
using System;

using WPFUtilities.Components.ServiceComponent;
using WPFUtilities.Components.Services.Command;

namespace WPFUtilities.Commands.Abstract
{
    /// <summary>
    /// command that requires service context
    /// </summary>
    public abstract class AbstractServiceCommand<ImplType, TParam1, TParam2> :
        AbstractServiceCommand<ImplType>,
        IServiceCommand<TParam1, TParam2>
        where ImplType : IServiceCommand<TParam1, TParam2>
    {
        /// <summary>
        /// creates a new instance
        /// </summary>
        /// <param name="serviceProvider">service component provider</param>
        public AbstractServiceCommand(IServiceComponentProvider serviceProvider)
            : base(serviceProvider) { }

        /// <inheritdoc/>
        public override void Execute(IServiceCommandExecuteContext context, object parameter)
        {
            if (parameter is object[] array)
            {
                if (array.Length > 2) throw new InvalidOperationException($"expected 2 parameters, but found {array.Length}");
                Execute(
                    context,
                    TransformParameter<TParam1>(0, array),
                    TransformParameter<TParam2>(1, array)
                    );
            }
        }

        /// <inheritdoc/>
        public abstract void Execute(IServiceCommandExecuteContext context, TParam1 param1, TParam2 param2);
    }
}
