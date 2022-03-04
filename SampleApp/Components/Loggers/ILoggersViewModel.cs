using System.ComponentModel;

using WPFUtilities.ComponentModels;

namespace SampleApp.Components.Loggers
{
    public interface ILoggersViewModel : IModelBase
    {
        /// <summary>
        /// selected provider
        /// </summary>
        object SelectedLogger { get; set; }

        /// <summary>
        /// providers
        /// </summary>
        BindingList<object> Loggers { get; }
    }
}
