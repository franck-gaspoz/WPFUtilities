using System.Windows;
using System.Windows.Controls;

using WPFUtilities.Extensions.DependencyObjects;

namespace SampleApp.Components.Data.KeyValue
{
    /// <summary>
    /// key value view
    /// </summary>
    public partial class KeyValueView : UserControl
    {
        /// <summary>
        /// creates a new instance
        /// </summary>
        public KeyValueView()
        {
            InitializeComponent();
        }

        /// <summary>
        /// key column header
        /// </summary>
        public object KeyColumnHeader
        {
            get { return (object)GetValue(KeyColumnHeaderProperty); }
            set { SetValue(KeyColumnHeaderProperty, value); }
        }

        /// <summary>
        /// key column header property
        /// </summary>
        public static readonly DependencyProperty KeyColumnHeaderProperty =
            DependencyObjectExtensions.Register(
                "KeyColumnHeader",
                typeof(object),
                typeof(KeyValueView),
                new PropertyMetadata("Key"));

        /// <summary>
        /// value column header
        /// </summary>
        public object ValueColumnHeader
        {
            get { return (object)GetValue(ValueColumnHeaderProperty); }
            set { SetValue(ValueColumnHeaderProperty, value); }
        }

        /// <summary>
        /// value column header property
        /// </summary>
        public static readonly DependencyProperty ValueColumnHeaderProperty =
            DependencyObjectExtensions.Register(
                "ValueColumnHeader",
                typeof(object),
                typeof(KeyValueView),
                new PropertyMetadata("Value"));
    }
}
