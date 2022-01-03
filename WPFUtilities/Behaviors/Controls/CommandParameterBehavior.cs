using System.Windows;
using System.Windows.Controls;

namespace WPFUtilities.Behaviors.Controls
{
    public class CommandParametersBehavior
    {
        public static CommandParametersBase GetCommandParameters(
            Control o)
        {
            return (CommandParametersBase)o.GetValue(CommandParametersProperty);
        }

        public static void SetCommandParameters(
          Control o, CommandParametersBase value)
        {
            o.SetValue(CommandParametersProperty, value);
        }

        public static readonly DependencyProperty CommandParametersProperty =
            DependencyProperty.RegisterAttached(
            "CommandParameters",
            typeof(CommandParametersBase),
            typeof(SelectionChangedCommandBehavior),
            new UIPropertyMetadata(
                null, Init));

        private static void Init(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {

        }
    }
}
