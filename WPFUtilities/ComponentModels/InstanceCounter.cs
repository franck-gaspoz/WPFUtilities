using System;
using System.Collections.Generic;

namespace WPFUtilities.ComponentModels
{
    /// <summary>
    /// instance per type counter helper
    /// </summary>
    public sealed class InstanceCounter
    {
        Dictionary<Type, int> _instanceCounter = new Dictionary<Type, int>();

        /// <summary>
        /// increment the counter for the type
        /// </summary>
        /// <param name="type">type related counter</param>
        /// <returns>incremented counter by 1 for the goiven type</returns>
        public int Increment(Type type)
        {
            if (!_instanceCounter.TryGetValue(type, out var count))
                _instanceCounter.Add(type, count);
            count++;
            _instanceCounter[type] = count;
            return count;
        }

        /// <summary>
        /// get the value of the counter for the type t
        /// </summary>
        /// <exception cref="ArgumentOutOfRangeException">no counter exists for the type</exception>
        /// <param name="t">type</param>
        /// <returns>int value of the counter</returns>
        public int this[Type t]
        {
            get => _instanceCounter[t];
        }
    }
}
