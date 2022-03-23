﻿using SampleApp.Components.Data.KeyValue;

namespace SampleApp.Components.ComponentHosts.Loggers
{
    /// <summary>
    /// logger view model
    /// </summary>
    public interface ILoggerViewModel : IKeyValueItem
    {
        /// <summary>
        /// logger group name
        /// </summary>
        string GroupName { get; set; }
    }
}