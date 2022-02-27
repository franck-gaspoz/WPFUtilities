
using WPFUtilities.Components.ServiceComponent;
using WPFUtilities.Components.Services.Command;

namespace WPFUtilities.Commands.Abstract
{
    /// <summary>
    /// command that requires service context
    /// </summary>
    public abstract class AbstractServiceCommand
        <ImplType, TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7> :
            AbstractServiceCommand<ImplType>,
            IServiceCommand<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7>
            where ImplType : IServiceCommand<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7>
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
            var array = ToValidParameterArray(context, parameter, 7);

            Execute(
                context,
                TransformParameter<TParam1>(0, array),
                TransformParameter<TParam2>(1, array),
                TransformParameter<TParam3>(2, array),
                TransformParameter<TParam4>(3, array),
                TransformParameter<TParam5>(4, array),
                TransformParameter<TParam6>(5, array),
                TransformParameter<TParam7>(6, array)
                );
        }

        /// <inheritdoc/>
        public abstract void Execute(IServiceCommandExecuteContext context, TParam1 param1, TParam2 param2, TParam3 param3, TParam4 param4, TParam5 param5, TParam6 param6, TParam7 param7);
    }
}
