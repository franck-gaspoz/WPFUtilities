﻿
using WPFUtilities.ComponentModels;

namespace SampleApp.Components.UI
{
    /// <summary>
    /// main window view model
    /// </summary>
    public interface IMainWindowViewModel : IModelBase
    {
        /// <summary>
        /// main window title
        /// </summary>
        string Title { get; }
    }
}
