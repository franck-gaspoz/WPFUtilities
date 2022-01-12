
using System.Windows;

using WPFUtilities.Components.Appl;

namespace WPFUtilities.ComponentModels
{
    /// <summary>
    /// singleton feature 
    /// <para>created by ApplicationHost</para>
    /// <para>application scoped singleton service</para>
    /// </summary>
    /// <typeparam name="T">service type</typeparam>
    public abstract class SingletonService<T>
    {
        static object _lock = new object();

        static T _instance;
        /// <summary>
        /// singleton instance
        /// </summary>
        public static T Instance
        {
            get
            {
                lock (_lock)
                {
                    if (_instance == null)
                        _instance =
                            (Application.Current is ApplicationBase app
                            && app != null &&
                            app.ApplicationHost != null) ?
                                (T)app.ApplicationHost.Host.Services.GetService(typeof(T))
                                : default(T);
                    return _instance;
                }
            }
        }
    }
}
