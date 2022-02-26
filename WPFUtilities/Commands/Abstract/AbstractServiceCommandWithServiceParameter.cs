using WPFUtilities.Components.Services.Command;

namespace WPFUtilities.Commands.Abstract
{
    public abstract class AbstractServiceCommandWithServiceParameter<ImplType, ServiceType>
        : AbstractCommandWithServiceParameter<ImplType, ServiceType>
        where ImplType : IServiceCommand
    {

    }
}
