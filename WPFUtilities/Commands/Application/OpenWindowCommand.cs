using System.Windows;

using WPFUtilities.Commands.Abstract;
using WPFUtilities.Components.ServiceComponent;

namespace WPFUtilities.Commands.Application
{
    /// <summary>
    /// open a window
    /// </summary>
    public class OpenWindowCommand : AbstractServiceParametricCommand<OpenWindowCommand, Window>
    {
        /// <inheritdoc/>
        public OpenWindowCommand(IServiceComponentProvider serviceProvider)
            : base(serviceProvider) { }

        /// <summary>
        /// open a window
        /// </summary>
        /// <param name="window">window</param>
        public override void Execute(Window window)
        {
            window.Show();
        }
    }
}
