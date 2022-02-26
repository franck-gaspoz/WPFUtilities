using System.Windows;

using WPFUtilities.Commands.Abstract;
using WPFUtilities.Components.ServiceComponent;
using WPFUtilities.Components.Services.Command;

namespace WPFUtilities.Commands.Application
{
    /// <summary>
    /// open a window
    /// </summary>
    public class OpenWindowCommand :
        AbstractServiceCommand<OpenWindowCommand, Window>
    {
        /// <inheritdoc/>
        public OpenWindowCommand(IServiceComponentProvider serviceProvider)
            : base(serviceProvider) { }

        /// <summary>
        /// open a window
        /// </summary>
        /// <param name="context">execute context</param>
        /// <param name="window">window</param>
        public override void Execute(
            IServiceCommandExecuteContext context,
            Window window
            )
        {
            window.Show();
        }
    }
}
