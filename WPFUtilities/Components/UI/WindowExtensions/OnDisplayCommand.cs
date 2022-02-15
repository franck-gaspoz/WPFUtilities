using System;
using System.Windows;
using System.Windows.Input;

using WPFUtilities.Components.Services.Properties;

using win = System.Windows;

namespace WPFUtilities.Components.UI.WindowExtensions
{
    /// <summary>
    /// on display invoke a command resolve from a service
    /// </summary>
    public static class OnDisplayCommand
    {
        #region property on display command

        /// <summary>
        /// get Type
        /// </summary>
        /// <param name="dependencyObject">dependency object</param>
        /// <returns>value</returns>
        public static Type GetType(DependencyObject dependencyObject) => (Type)dependencyObject.GetValue(TypeProperty);

        /// <summary>
        /// set Type
        /// </summary>
        /// <param name="dependencyObject">dependency Object</param>
        /// <param name="value">value</param>
        public static void SetType(DependencyObject dependencyObject, Type value) => dependencyObject.SetValue(TypeProperty, value);

        /// <summary>
        /// Type property
        /// </summary>
        public static readonly DependencyProperty TypeProperty =
            DependencyProperty.Register(
                "Type",
                typeof(Type),
                typeof(win.Window),
                new UIPropertyMetadata(null, TypeChanged));

        #endregion

        private static void TypeChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs eventArgs)
        {
            if (dependencyObject is win.Window win)
            {
                var commandType = (Type)win.GetValue(TypeProperty);
                win.WithService(
                    win, commandType,
                    (command) =>
                    {
                        (command as ICommand)?.Execute(win);
                    });
            }
        }
    }
}
