﻿using System;
using System.Windows;

namespace WPFUtilities.Commands.Interactivity
{
    /// <summary>
    /// close a window
    /// </summary>
    public class CloseWindowCommand
        : AbstractCommand<CloseWindowCommand>
    {
        /// <inheritdoc/>
        public override bool CanExecute(object parameter) => true;

        /// <summary>
        /// close the window
        /// </summary>
        /// <param name="parameter">a window instance or a Func&lt;Window&gt;</param>
        public override void Execute(object parameter)
        {
            if (parameter is Window win
                && win != null)
                win.Close();
            else
            {
                var a = (Func<Window>)parameter;
                var w = a?.Invoke();
                w.Close();
            }
        }
    }
}