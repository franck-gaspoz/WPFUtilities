using System;
using System.Linq;
using System.Reflection;
using System.Windows;

using WPFUtilities.Commands.Abstract;

namespace WPFUtilities.Commands.Interactivity
{
    /// <summary>
    /// open a dialog window
    /// </summary>
    [Obsolete]
    public class OpenWindowCommand : AbstractCommand<OpenWindowCommand>
    {
        /// <summary>
        /// open a window
        /// </summary>
        /// <param name="parameter">class name (string) or Type</param>
        public override void Execute(object parameter)
        {
            Type t = null;
            if (parameter is string name)
            {
                t = Assembly
                    .GetEntryAssembly()
                    .DefinedTypes
                    .Where(x => x.Name == (string)parameter)
                    .FirstOrDefault();
            }

            if (parameter is Type ty)
                t = ty;

            if (t != null)
            {
                var w = (Window)Activator.CreateInstance(t);
                var r = w.ShowDialog();
            }
        }
    }
}
