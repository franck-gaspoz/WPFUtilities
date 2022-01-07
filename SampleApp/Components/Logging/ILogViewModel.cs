using System.ComponentModel;

using WPFUtilities.ComponentModels;

namespace SampleApp.Components.Logging
{
    /// <summary>
    /// a log view model
    /// </summary>
    public interface ILogViewModel : IModelBase
    {
        /// <summary>
        /// messages
        /// </summary>
        BindingList<string> Messages { get; }
    }
}
