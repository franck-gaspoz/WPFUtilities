using System;

using WPFUtilities.Components.ServiceComponent;
using WPFUtilities.Components.Services.Command;

namespace WPFUtilities.Commands.Abstract
{
    /// <summary>
    /// abstract service command having a service parameter
    /// </summary>
    /// <typeparam name="ImplType">command implementation type</typeparam>
    /// <typeparam name="ServiceType">parameter service type</typeparam>
    public abstract class AbstractServiceCommandWithServiceParameter<ImplType, ServiceType>
        : AbstractCommandWithServiceParameter<ImplType, ServiceType>,
        IServiceCommand
        where ImplType : IServiceCommand
    {
        /// <summary>
        /// creates a new instance
        /// </summary>
        /// <param name="serviceProvider">service provider</param>
        public AbstractServiceCommandWithServiceParameter(IServiceComponentProvider serviceProvider)
            : base(serviceProvider) { }

        /// <inheritdoc/>
        public override void Execute(ServiceType service)
            => throw new InvalidOperationException("method not implemented. use Execute(ServiceType parameter, IServiceCommandExecuteContext context) instead");

        /// <inheritdoc/>
        public void Execute(object parameter, IServiceCommandExecuteContext context)
        {
            if (!(parameter is Type serviceType)) throw new InvalidOperationException($"expected a parameter of type '{typeof(ServiceType).FullName}', but found {parameter.GetType().FullName}");
            var service = (ServiceType)ServiceProvider.GetRequiredService(serviceType);
            Execute(service, context);
        }

        /// <summary>
        /// execute the command
        /// </summary>
        /// <param name="parameter">service parameter</param>
        /// <param name="context">service command execute context</param>
        public abstract void Execute(ServiceType parameter, IServiceCommandExecuteContext context);
    }
}

