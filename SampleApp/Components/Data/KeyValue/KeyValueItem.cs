using WPFUtilities.ComponentModels;

namespace SampleApp.Components.Data.KeyValue
{
    /// <inheritdoc/>
    public class KeyValueItem : ModelBase, IKeyValueItem
    {
        /// <inheritdoc/>
        public string Key { get; set; }

        /// <inheritdoc/>
        public string Value { get; set; }
    }
}
