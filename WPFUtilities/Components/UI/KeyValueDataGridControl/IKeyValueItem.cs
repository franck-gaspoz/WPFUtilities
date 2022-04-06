using WPFUtilities.ComponentModels;

namespace WPFUtilities.Components.UI.KeyValueDataGridControl
{
    /// <summary>
    /// key / value data item
    /// </summary>
    public interface IKeyValueItem : IModelBase
    {
        /// <summary>
        /// key
        /// </summary>
        string Key { get; set; }

        /// <summary>
        /// value
        /// </summary>
        string Value { get; set; }
    }
}
