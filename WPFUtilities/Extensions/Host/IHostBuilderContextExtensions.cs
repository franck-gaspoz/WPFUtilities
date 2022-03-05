using Microsoft.Extensions.Hosting;

namespace WPFUtilities.Extensions.Host
{
    /// <summary>
    /// host builder extensions
    /// </summary>
    public static class IHostBuilderContextExtensions
    {
        /// <summary>
        /// get host from creating host builder context
        /// TODO: check is well implemented
        /// </summary>
        /// <exception cref="System.Collections.Generic.KeyNotFoundException">host builder context not initialized</exception>
        /// <param name="hostBuilderContext">creating host builder context</param>
        /// <returns>host</returns>
        public static IHost GetHost(this HostBuilderContext hostBuilderContext)
            => (IHost)hostBuilderContext.Properties[typeof(IHost)];
    }
}
