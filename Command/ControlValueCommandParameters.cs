using System.Windows;
using System.Windows.Controls;

namespace WPFUtilities.Command
{
    public class ControlValueCommandParameters
        : CommandParametersBase,
        IControlValueCommandParameters
    {
        public object Value { get; set; }

        public Control Control { get; set; }

        public Window Window { get; set; }
    }
}
