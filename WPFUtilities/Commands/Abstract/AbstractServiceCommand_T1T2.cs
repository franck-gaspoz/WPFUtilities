
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
        public override void Execute(object parameter, IServiceCommandExecuteContext context)
        {
            if (parameter is object[] array)
            {
                if (array.Length != 2) throw new InvalidOperationException($"expected 2 parameters, but found {array.Length}");
                Execute(
                    ConvertTo<TParam1>(array[0]),
                    ConvertTo<TParam2>(array[1]),
                    context);
            }
        }

        T ConvertTo<T>(object parameter)
        {
            if (parameter is Type type)
                return (T)ServiceProvider.GetService(type);
            return (T)parameter;
        }

        /// <inheritdoc/>
        public abstract void Execute(TParam1 param1, TParam2 param2, IServiceCommandExecuteContext context);
    }
}
