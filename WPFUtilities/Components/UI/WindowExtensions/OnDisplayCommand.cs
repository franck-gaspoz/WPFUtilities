using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;

using WPFUtilities.Extensions.Services;

using win = System.Windows;

namespace WPFUtilities.Components.UI
{
    /// <summary>
    /// on display invoke a command resolve from a service
    /// </summary>
    public static partial class Window
    {
        #region property on display command

        /// <summary>
        /// get Type
        /// </summary>
        /// <param name="dependencyObject">dependency object</param>
        /// <returns>value</returns>
        public static Type GetOnDisplayCommand(DependencyObject dependencyObject) => (Type)dependencyObject.GetValue(OnDisplayCommandProperty);

        /// <summary>
        /// set OnDisplayCommand
        /// </summary>
        /// <param name="dependencyObject">dependency Object</param>
        /// <param name="value">value</param>
        public static void SetOnDisplayCommand(DependencyObject dependencyObject, Type value) => dependencyObject.SetValue(OnDisplayCommandProperty, value);

        /// <summary>
        /// OnDisplayCommand property
        /// </summary>
        public static readonly DependencyProperty OnDisplayCommandProperty =
            DependencyProperty.Register(
                "OnDisplayCommand",
                typeof(Type),
                typeof(win.Window),
                new UIPropertyMetadata(null, OnDisplayCommandChanged));

        #endregion

        private static void OnDisplayCommandChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs eventArgs)
        {
            if (DesignerProperties.GetIsInDesignMode(dependencyObject)) return;
            if (!(dependencyObject is win.Window win)) return;

            var commandType = (Type)win.GetValue(OnDisplayCommandProperty);
            win.WithService(
                win,
                commandType,
                (command) =>
                {
                    (command as ICommand)?.Execute(win);
                });
        }
    }
}
