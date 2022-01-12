using Microsoft.Extensions.Hosting;

namespace WPFUtilities.Components.Component
{
    /// <summary>
    /// a service type (DI) component
    /// </summary>
    public interface IServiceComponent
    {
        /// <summary>
        /// component host
        /// </summary>
        IHost Host { get; }

        /// <summary>
        /// configure services dependencies for owned host
        /// </summary>
        void Configure();

        /// <summary>
        /// build the owned host
        /// </summary>
        void Build();
    }
}
