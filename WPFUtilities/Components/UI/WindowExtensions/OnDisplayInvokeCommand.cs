using System;
using System.Windows;
using System.Windows.Input;

using Microsoft.Xaml.Behaviors;

using WPFUtilities.Extensions.Behaviors;

using win = System.Windows;

namespace WPFUtilities.Components.UI.WindowExtensions
{
    /// <summary>
    /// call delegate command when a window is totally displayed (visible on screen)
    /// </summary>
    [Obsolete]
    public class OnDisplayInvokeCommand
        : Behavior<win.Window>
    {
        #region Command

        /// <summary>
        /// command
        /// </summary>
        public ICommand Command { get; set; }

        /// <inheritdoc/> 
        protected override void OnAttached()
        {
            AssociatedObject.IsVisibleChanged += Window_IsVisibleChanged;
        }

        #endregion

        private static void Window_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (sender is win.Window win)
            {
                void InvokeCommand(object o, EventArgs eventArgs)
                {
                    win.Loaded -= InvokeCommand;
                    var behavior = win.GetBehavior<OnDisplayInvokeCommand>();
                    behavior?.Command.Execute(win);
                }
                win.Loaded += InvokeCommand;
            }
        }
    }
}
