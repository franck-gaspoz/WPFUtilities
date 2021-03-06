using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Input;

using WPFUtilities.Components.ServiceComponent;
using WPFUtilities.Components.Services.Command;
using WPFUtilities.Components.Services.Command.Attributes;
using WPFUtilities.Components.Services.Properties;

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
                {
                    if (parameter != null)
                        throw CreateExecuteCommandParamaterTypeErrorException(
                            paramInfo.ParameterType, parameter.GetType(), index, "Parameter default value transformation error: ");
                    else
                        return default(T);
                }
                else
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

        MethodInfo GetExecuteMethod()
        {
            var executeMethods = this.GetType().GetMethods()
                .Where(x => x.Name == "Execute")
                .ToDictionary(x => x.GetParameters().Length);
            var executeMethod = executeMethods[executeMethods.Keys.Max()];
            return executeMethod;
        }

        ParameterInfo GetParamInfo(int index)
        {
            var executeMethod = GetExecuteMethod();
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
            {
                throw CreateExecuteCommandParamaterTypeErrorException(
                    paramInfo.ParameterType, parameter.GetType(), index);
            }
            return (T)parameter;
        }

        /// <summary>
        /// returns an array from parameter object
        /// <para>- if parameter is array of object returns parameter</para>
        /// <para>- if parameter is single object and Execute expects one parameter returns array of the object</para>
        /// <para>- if parameter is single object and Execute expects more that one parameter, try to map object to Execute parameters and to build the method parameters array</para>
        /// </summary>
        /// <param name="context">context</param>
        /// <param name="maxLength">max array length</param>
        /// <param name="parameter">parameter</param>
        /// <returns>parameter or array with parameter inside</returns>
        /// <exception cref="InvalidOperationException">parameter object doesn't match command Execute method</exception>
        protected object[] ToParameterArray(
            IServiceCommandExecuteContext context,
            int maxLength,
            object parameter)
        {
            if (parameter is object[] array) return array;

            if (context.Caller is DependencyObject dependencyObject)
            {
                int curLength = 1;
                var parameters = new List<object> { parameter };
                // extra command parameters are used as parameters source                
                var param = dependencyObject.GetValue(Command.Param2Property);
                if (param != Command.UnsetPropertyValue)
                {
                    var properties = new List<DependencyProperty> {
                        Command.Param2Property,
                        Command.Param3Property,
                        Command.Param4Property,
                        Command.Param5Property,
                        Command.Param6Property,
                        Command.Param7Property,
                        Command.Param8Property,
                        Command.Param9Property,
                        Command.Param10Property
                    };
                    foreach (var property in properties)
                    {
                        param = dependencyObject.GetValue(property);
                        if (param != Command.UnsetPropertyValue)
                        {
                            parameters.Add(param);
                            curLength++;
                        }
                        else
                            break;
                        if (curLength == maxLength) break;
                    }
                    return parameters.ToArray();
                }
            }

            if (parameter != null)
            {
                // ServiceCommandParameters attributed object is mapped to Execute method parameters
                if (parameter.GetType()
                    .GetCustomAttribute<ServiceCommandParametersAttribute>() != null)
                {
                    var executeMethod = GetExecuteMethod();
                    if (executeMethod.GetParameters().Length > 2)
                    {
                        // min for _T1T2 service commands
                        var parameters = new List<object>();
                        var properties = parameter.GetType().GetProperties();
                        foreach (var paramInfo in executeMethod.GetParameters())
                        {
                            if (paramInfo.ParameterType != typeof(IServiceCommandExecuteContext))
                            {
                                var stringComparison = StringComparison.InvariantCultureIgnoreCase;
                                var propertyInfos = properties
                                    .Where(x => x.Name.Equals(paramInfo.Name, stringComparison))
                                    .ToList();
                                if (propertyInfos.Count == 0)
                                    throw CreateParameterObjectDoesntMatchExecuteMethodException(
                                        $"property {paramInfo.Name} is missing in object parameter");
                                if (propertyInfos.Count > 1)
                                    throw CreateParameterObjectDoesntMatchExecuteMethodException(
                                        $"property {paramInfo.Name} has several matchin properties in object parameter (match mode is: {stringComparison})");

                                parameters.Add(propertyInfos[0].GetValue(parameter));
                            }
                        }
                        return parameters.ToArray();
                    }
                }
            }
            return new object[] { parameter };
        }

        Exception CreateParameterObjectDoesntMatchExecuteMethodException(string message)
            => new InvalidOperationException("parameter object doesn't match command Execute method:" + message);

        /// <summary>
        /// validate a parameter array
        /// </summary>
        /// <param name="context">context</param>
        /// <param name="parameters">parameters</param>
        /// <param name="maxLength">max length limit</param>
        /// <param name="minLength">min length limit (default 0)</param>
        /// <returns>parameters array</returns>
        /// <exception cref="InvalidOperationException">parameters array length is over max length limit</exception>
        /// <exception cref="InvalidOperationException">parameters array length is under min length limit</exception>
        protected object[] ToValidParameterArray(
            IServiceCommandExecuteContext context,
            object parameters,
            int maxLength,
            int minLength = 0)
        {
            var array = ToParameterArray(context, maxLength, parameters);
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
