
using WPFUtilities.ComponentModels;
using WPFUtilities.Components.Component;

namespace WPFUtilities.Components.Application
{
    /// <summary>
    /// application host
    /// </summary>
    public interface IApplicationHost :
        IComponentHost,
        IServicesConfigurator
    {
        /// <summary>
        /// configure the host
        /// </summary>
        /// <param name="applicationBaseSettings">application base settings</param>
        void Configure(IApplicationBaseSettings applicationBaseSettings);
    }
}
