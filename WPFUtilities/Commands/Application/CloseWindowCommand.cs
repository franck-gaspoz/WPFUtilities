using System;
using System.Windows;

using Microsoft.Extensions.DependencyInjection;

namespace WPFUtilities.Commands.Application
{
    /// <summary>
    /// close a window
    /// </summary>
    public class CloseWindowCommand : AbstractCommand<CloseWindowCommand>
    {
        IServiceProvider _serviceProvider;

        /// <summary>
        /// creates a new command working with the specified service provider
        /// </summary>
        /// <param name="serviceProvider">service provider</param>
        public CloseWindowCommand(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        /// <summary>
        /// close a window belonging a services scope
        /// </summary>
        /// <param name="parameter">array of: [dependency object caller,window interface type]</param>
        public override void Execute(object parameter)
        {
            if (parameter is Type type)
            {
                var window = (Window)_serviceProvider.GetRequiredService(type);
                window.Close();
            }
        }
    }
}
