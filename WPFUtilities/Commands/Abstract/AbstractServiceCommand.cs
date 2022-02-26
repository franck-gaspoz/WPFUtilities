using System;
using System.Linq;
using System.Windows.Input;

using WPFUtilities.Components.ServiceComponent;
using WPFUtilities.Components.Services.Command;
using WPFUtilities.Components.Services.Command.Attributes;

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

        /// <summary>
        /// cast a parameter to the specified type according to the Execute method parameters custom attributes
        /// </summary>
        /// <typeparam name="T">target type</typeparam>
        /// <param name="index">parameter index</param>
        /// <param name="parameter">source parameter</param>
        /// <returns></returns>
        protected T CastParameterTo<T>(int index, object parameter)
        {
            if (parameter == null) return default(T);

            var executeMethods = this.GetType().GetMethods()
                .Where(x => x.Name == "Execute")
                .ToDictionary(x => x.GetParameters().Length);
            var executeMethod = executeMethods[executeMethods.Keys.Max()];
            var paramInfo = executeMethod.GetParameters()[index];
            {
                if (parameter is Type type
                    && (paramInfo.ParameterType != typeof(Type)
                    || paramInfo.GetCustomAttributes(typeof(ServiceReferenceAttribute), true).Any()))
                    // resolve type as a service reference
                    return (T)ServiceProvider.GetService(type);
            }

            return (T)parameter;
        }
    }
}
