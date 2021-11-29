using System.Windows;
using System.Windows.Controls;

namespace WPFUtilities.Command
{
    public interface IControlValueCommandParameters
    {
        object Value { get; set; }

        Control Control { get; set; }

        Window Window { get; set; }
    }
}
