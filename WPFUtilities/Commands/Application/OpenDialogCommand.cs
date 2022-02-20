using System.Windows;

using WPFUtilities.Commands.Abstract;
using WPFUtilities.Components.ServiceComponent;

namespace WPFUtilities.Commands.Application
{
    /// <summary>
    /// open a dialog window
    /// </summary>
    public class OpenDialogCommand : AbstractServiceParametricCommand<OpenWindowCommand, Window>
    {
        /// <inheritdoc/>
        public OpenDialogCommand(IServiceComponentProvider serviceProvider)
            : base(serviceProvider) { }

        /// <summary>
        /// open a dialog window
        /// </summary>
        /// <param name="window">window</param>
        public override void Execute(Window window)
        {
            window.ShowDialog();
        }
    }
}
