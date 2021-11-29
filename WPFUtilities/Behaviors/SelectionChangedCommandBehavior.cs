using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using WPFUtilities.Command;

namespace WPFUtilities.Behaviors
{
    public class SelectionChangedCommandBehavior
    {
        public static ICommand GetSelectionChangedCommand(
            ComboBox o)
        {
            if (DesignerProperties.GetIsInDesignMode(o))
                return null;
            return (ICommand)o.GetValue(SelectionChangedCommandProperty);
        }

        public static void SetSelectionChangedCommand(
          ComboBox o, ICommand value)
        {
            if (DesignerProperties.GetIsInDesignMode(o))
                return;
            o.SetValue(SelectionChangedCommandProperty, value);
        }

        public static readonly DependencyProperty SelectionChangedCommandProperty =
            DependencyProperty.RegisterAttached(
            "SelectionChangedCommand",
            typeof(ICommand),
            typeof(SelectionChangedCommandBehavior),
            new UIPropertyMetadata(
                null,
                Init));

        static void Init(
              DependencyObject depObj,
              DependencyPropertyChangedEventArgs e)
        {
            if (!(depObj is ComboBox o))
                return;
            if (e.NewValue == null)
                return;
            o.SelectionChanged += O_SelectionChanged;
        }

        static void O_SelectionChanged(
            object sender, 
            SelectionChangedEventArgs e)
        {
            if (sender is ComboBox o)
            {
                var p = o.GetValue(
                    CommandParametersBehavior.CommandParametersProperty);
                if (p == null)
                    throw new Exception($"usage of behavior {nameof(SelectionChangedCommandBehavior)} on object {o} needs DependencyProperty {nameof(CommandParametersBehavior.CommandParametersProperty)}");
                if (!(p is IControlValueCommandParameters param))
                    throw new Exception($"command parameters object type must match interface {nameof(IControlValueCommandParameters)}");
                param.Control = o;
                param.Value = o.SelectedValue;
                param.Window = WPFHelper
                    .FindAncestor<Window>(o);
                var cmd = GetSelectionChangedCommand(o);
                if (cmd!=null)
                {
                    cmd.Execute(param);
                }
            }
        }
    }
}

