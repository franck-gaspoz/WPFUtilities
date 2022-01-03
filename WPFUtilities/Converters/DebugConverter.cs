﻿using System;
using System.Globalization;
using System.Windows.Data;

using WPFUtilities.ComponentModel;

namespace WPFUtilities.Converters
{
    /// <summary>
    /// multi-purpose debug converter
    /// </summary>
    public class DebugConverter
        : Singleton<DebugConverter>, IValueConverter
    {
        static int _instanceCount = 0;

        static Action<DebugConverter> CallBack;

        /// <summary>
        /// build a new instance, update instance counter
        /// </summary>
        public DebugConverter()
        {
            _instanceCount++;
        }

        /// <summary>
        /// convert
        /// </summary>
        /// <param name="value">value</param>
        /// <param name="targetType">target ype</param>
        /// <param name="parameter">parameter</param>
        /// <param name="culture">culture</param>
        /// <returns>value</returns>
        public object Convert(
            object value,
            Type targetType,
            object parameter,
            CultureInfo culture)
        {
            CallBack?.Invoke(this);
            return value;
        }

        /// <summary>
        /// convert back value
        /// </summary>
        /// <param name="value">value</param>
        /// <param name="targetType">target type</param>
        /// <param name="parameter">parameter</param>
        /// <param name="culture">culture</param>
        /// <returns>value</returns>
        public object ConvertBack(
            object value,
            Type targetType,
            object parameter,
            CultureInfo culture)
        {
            CallBack?.Invoke(this);
            return value;
        }
    }
}
