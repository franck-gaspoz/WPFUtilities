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
    public class OpenDialogCommand :
        AbstractServiceCommand<
            OpenDialogCommand,
            Window, bool>
    {
        /// <inheritdoc/>
        public OpenDialogCommand(IServiceComponentProvider serviceProvider)
            : base(serviceProvider) { }

        /// <summary>
        /// show a dialog window command
        /// </summary>
        /// <param name="context">execute context</param>
        /// <param name="window">window to be shown</param>
        /// <param name="takesOwnership">set owner from parent caller window if true</param>
        public override void Execute(
            IServiceCommandExecuteContext context,
            Window window,
            bool takesOwnership = true
            )
        {
            if (window.WindowStartupLocation == WindowStartupLocation.CenterOwner
                && takesOwnership
                && context.Caller is DependencyObject dependencyObject)
                window.Owner = WPFHelper.FindAncestor<Window>(dependencyObject);

            window.ShowDialog();
        }
    }
}
