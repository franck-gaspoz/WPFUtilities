using System;
using System.Windows.Input;

using WPFUtilities.Components.ServiceComponent;

namespace WPFUtilities.Commands.Abstract
{
    /// <summary>
    /// command that requires service context and a service parameter
    /// </summary>
    public abstract class AbstractCommandWithServiceParameter<ImplType, ServiceType>
        : AbstractServiceCommand<ImplType>,
        ICommand
        where ImplType : ICommand
    {
        /// <summary>
        /// creates a new instance working with the specified service provider
        /// </summary>
        /// <param name="serviceProvider">service component provider</param>
        public AbstractCommandWithServiceParameter(IServiceComponentProvider serviceProvider)
            : base(serviceProvider) { }

        /// <summary>
        /// executes the command with an unique parameter 'service type' that is resolved from services
        /// </summary>
        /// <param name="parameter">parameter</param>
        public override void Execute(object parameter)
        {
            if (!(parameter is Type serviceType)) throw new InvalidOperationException($"expected a parameter of type '{typeof(ServiceType).FullName}', but found {parameter.GetType().FullName}");
            var service = (ServiceType)ServiceProvider.GetRequiredService(serviceType);
            Execute(service);
        }

        /// <summary>
        /// executes the command
        /// </summary>
        /// <param name="service">the expected service needed to executes the command</param>
        public abstract void Execute(ServiceType service);
    }
}
