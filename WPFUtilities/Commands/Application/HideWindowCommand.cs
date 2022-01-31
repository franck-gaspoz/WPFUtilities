using System.Windows;

using WPFUtilities.Commands.Abstract;
using WPFUtilities.Components.ServiceComponent;

namespace WPFUtilities.Commands.Application
{
    /// <summary>
    /// close a window
    /// </summary>
    public class HideWindowCommand : AbstractServiceParametricCommand<HideWindowCommand, Window>
    {
        /// <summary>
        /// creates a new command working with the specified service provider
        /// </summary>
        /// <param name="serviceProvider">service component provider</param>
        public HideWindowCommand(IServiceComponentProvider serviceProvider)
            : base(serviceProvider) { }

        /// <summary>
        /// hide a window
        /// </summary>
        /// <param name="window">window</param>
        public override void Execute(Window window)
            => window.Hide();
    }
}
