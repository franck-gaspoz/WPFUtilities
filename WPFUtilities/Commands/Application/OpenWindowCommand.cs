﻿using System.Windows;

using WPFUtilities.Commands.Abstract;
using WPFUtilities.Components.ServiceComponent;
using WPFUtilities.Components.Services.Command;

namespace WPFUtilities.Commands.Application
{
    /// <summary>
    /// open a window
    /// </summary>
    public class OpenWindowCommand :
        AbstractServiceCommand<OpenWindowCommand, Window, bool>
    {
        /// <inheritdoc/>
        public OpenWindowCommand(IServiceComponentProvider serviceProvider)
            : base(serviceProvider) { }

        /// <summary>
        /// open a window
        /// </summary>
        /// <param name="window">window</param>
        /// <param name="setOwner">set owner if true</param>
        /// <param name="context">execute context</param>
        public override void Execute(
            Window window,
            bool setOwner,
            IServiceCommandExecuteContext context)
        {
            window.Show();
        }
    }
}
