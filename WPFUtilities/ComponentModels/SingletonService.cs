using System;

using WPFUtilities.Components.Appl;

namespace WPFUtilities.ComponentModels
{
    /// <summary>
    /// singleton feature using ApplicationHost and Dependency Injector
    /// </summary>
    /// <typeparam name="T">class</typeparam>
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
                            ApplicationHost.Instance.Host == null ?
                                (T)Activator.CreateInstance(typeof(T))
                                : (T)ApplicationHost.Instance.Host.Services.GetService(typeof(T));
                    return _instance;
                }
            }
        }
    }
}
