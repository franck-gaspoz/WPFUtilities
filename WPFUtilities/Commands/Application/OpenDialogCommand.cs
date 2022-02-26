﻿
using System.Windows;

using WPFUtilities.Commands.Abstract;
using WPFUtilities.Components.ServiceComponent;
using WPFUtilities.Components.Services.Command;
using WPFUtilities.Helpers;

namespace WPFUtilities.Commands.Application
{
    /// <summary>
    /// open a dialog window
    /// </summary>
    public class OpenDialogCommand : AbstractServiceCommand<OpenDialogCommand, Window>
    {
        /// <inheritdoc/>
        public OpenDialogCommand(IServiceComponentProvider serviceProvider)
            : base(serviceProvider) { }

        /// <summary>
        /// show a dialog window command
        /// </summary>
        /// <param name="window">window to be shown</param>
        /// <param name="context">execute context</param>
        public override void Execute(Window window, IServiceCommandExecuteContext context)
        {
            if (window.WindowStartupLocation == WindowStartupLocation.CenterOwner
                && context.Caller is DependencyObject dependencyObject)
                window.Owner = WPFHelper.FindAncestor<Window>(dependencyObject);

            window.ShowDialog();
        }
    }
}