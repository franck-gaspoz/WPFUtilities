using System;
using System.Linq;
using System.Reflection;
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
        /// transform a parameter to the specified type according to the Execute method parameters default value
        /// </summary>
        /// <typeparam name="T">target type</typeparam>
        /// <param name="index">parameter index</param>
        /// <returns>parameter as T or service resolve from service reference of type T</returns>
        /// <exception cref="InvalidOperationException">parameter wrong type error</exception>
        protected T TransformParameter<T>(int index)
        {
            var paramInfo = GetParamInfo(index);
            if (paramInfo.HasDefaultValue)
            {
                var parameter = paramInfo.DefaultValue;
                if (!(parameter is T prameterAsT))
                    throw CreateExecuteCommandParamaterTypeErrorException(
                        paramInfo.ParameterType, parameter.GetType(), index, "Parameter default value transformation error: ");
                return prameterAsT;
            }
            throw CreateExecuteCommandParameterDefaultMissingException(typeof(T), index);
        }

        /// <summary>
        /// transform parrameter depending on index et parameters array
        /// </summary>
        /// <typeparam name="T">target type</typeparam>
        /// <param name="index">parameter index</param>
        /// <param name="paramArray">array of parameters values</param>
        /// <returns>parameter as T or service resolve from service reference of type T</returns>
        /// <exception cref="InvalidOperationException">parameter wrong type error</exception>
        protected T TransformParameter<T>(int index, object[] paramArray)
        {
            if (paramArray.Length > index)
                return TransformParameter<T>(index, paramArray[index]);
            else
                return TransformParameter<T>(index);
        }

        ParameterInfo GetParamInfo(int index)
        {
            var executeMethods = this.GetType().GetMethods()
                .Where(x => x.Name == "Execute")
                .ToDictionary(x => x.GetParameters().Length);
            var executeMethod = executeMethods[executeMethods.Keys.Max()];
            var paramInfo = executeMethod.GetParameters()[index + 1];
            return paramInfo;
        }

        /// <summary>
        /// transform a parameter to the specified type according to the Execute method parameters custom attributes
        /// </summary>
        /// <typeparam name="T">target type</typeparam>
        /// <param name="index">parameter index</param>
        /// <param name="parameter">source parameter</param>
        /// <returns>parameter as T or service resolve from service reference of type T</returns>
        /// <exception cref="InvalidOperationException">parameter wrong type error</exception>
        protected T TransformParameter<T>(int index, object parameter)
        {
            if (parameter == null) return default(T);

            var paramInfo = GetParamInfo(index);

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

        /// <summary>
        /// returns an array from parameter object, if parameter is array of object returns parameter
        /// </summary>
        /// <param name="parameter">parameter</param>
        /// <returns>parameter or array with parameter inside</returns>
        protected object[] ToParameterArray(object parameter)
            => (!(parameter is object[] array))
                ? new object[] { parameter }
                : array;

        /// <summary>
        /// validate a parameter array
        /// </summary>
        /// <param name="parameters">parameters</param>
        /// <param name="maxLength">max length limit</param>
        /// <param name="minLength">min length limit (default 0)</param>
        /// <returns>parameters array</returns>
        /// <exception cref="InvalidOperationException">parameters array length is over max length limit</exception>
        /// <exception cref="InvalidOperationException">parameters array length is under min length limit</exception>
        protected object[] ToValidParameterArray(
            object parameters,
            int maxLength,
            int minLength = 0)
        {
            var array = ToParameterArray(parameters);
            if (array.Length > maxLength) throw new InvalidOperationException($"expected maximim {maxLength} parameters, but found {array.Length}");
            if (array.Length < minLength) throw new InvalidOperationException($"expected minimum {minLength} parameters, but found {array.Length}");
            return array;
        }

        InvalidOperationException CreateExecuteCommandParamaterTypeErrorException(Type expectedType, Type type, int index, string prefix = "")
            => new InvalidOperationException($"{prefix}Excute command '{this.GetType().Name}' parameter {index} wrong type: expected {expectedType.FullName} but found {type.FullName}");

        InvalidOperationException CreateExecuteCommandParameterDefaultMissingException(Type expectedType, int index, string prefix = "")
           => new InvalidOperationException($"{prefix}Execute command '{this.GetType().Name}' parameter {index} has no default value, but value was missing in call");

    }
}
