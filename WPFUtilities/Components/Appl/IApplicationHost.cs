using Microsoft.Extensions.Hosting;

namespace WPFUtilities.Components.Appl
{
    /// <summary>
    /// application host
    /// </summary>
    public interface IApplicationHost
    {
        /// <summary>
        /// host
        /// </summary>
        IHost Host { get; }

        /// <summary>
        /// build host
        /// </summary>
        /// <param name="applicationBaseSettings">application base settings</param>
        void Build(IApplicationBaseSettings applicationBaseSettings);
    }
}
