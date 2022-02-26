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
        /// <param name="context">execute context</param>
        /// <param name="parameter">parameter</param>
        public abstract void Execute(IServiceCommandExecuteContext context, object parameter);

        /// <summary>
        /// cast a parameter to the specified type according to the Execute method parameters custom attributes
        /// </summary>
        /// <typeparam name="T">target type</typeparam>
        /// <param name="index">parameter index</param>
        /// <param name="parameter">source parameter</param>
        /// <returns>parameter as T or service resolve from service reference of type T</returns>
        protected T CastParameterTo<T>(int index, object parameter)
        {
            if (parameter == null) return default(T);

            var executeMethods = this.GetType().GetMethods()
                .Where(x => x.Name == "Execute")
                .ToDictionary(x => x.GetParameters().Length);
            var executeMethod = executeMethods[executeMethods.Keys.Max()];
            var paramInfo = executeMethod.GetParameters()[index];

            // TODO: test with interface type
            if (parameter is Type type
                && (paramInfo.ParameterType != typeof(Type)
                || paramInfo.GetCustomAttributes(typeof(ServiceReferenceAttribute), true).Any()))
            {
                // resolve type as a service reference
                var service = ServiceProvider.GetService(type);
                if (!(service is T))
                    throw CreateExecuteCommandParamaterTypeErrorException(
                        paramInfo.ParameterType, service.GetType(), index,
                        "Service parameter resolve error: ");
                return (T)service;
            }

            if (!(parameter is T))
                throw CreateExecuteCommandParamaterTypeErrorException(
                    paramInfo.ParameterType, parameter.GetType(), index);
            return (T)parameter;
        }

        InvalidOperationException CreateExecuteCommandParamaterTypeErrorException(Type expectedType, Type type, int index, string prefix = "")
            => new InvalidOperationException($"{prefix}Excute command '{this.GetType().Name}' parameter {index} wrong type: expected {expectedType.FullName} but found {type.FullName}");
    }
}
