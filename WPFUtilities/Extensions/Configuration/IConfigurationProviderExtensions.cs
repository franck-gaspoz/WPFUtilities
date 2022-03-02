using System.Collections.Generic;

using Microsoft.Extensions.Configuration;

using WPFUtilities.Extensions.Reflections;

namespace WPFUtilities.Extensions.Configuration
{
    /// <summary>
    /// IConfigurationProvider extensions
    /// </summary>
    public static class IConfigurationProviderExtensions
    {
        /// <summary>
        /// returns internal data
        /// </summary>
        /// <param name="provider">configuration provider</param>
        /// <param name="data">provierds internal data or null is doesn't exists</param>
        /// <returns>true if have a result, false otherwise</returns>
        public static bool TryGetData(this IConfigurationProvider provider,
            out Dictionary<string, string> data)
        {
            if (provider.GetProperty<Dictionary<string, string>>("Data", out var internalData))
            {
                data = internalData;
                return true;
            }
            data = null;
            return false;
        }
    }
}
