using System;
using System.Collections.Generic;
using System.Windows;

namespace WPFUtilities.Helpers
{
    /// <summary>
    /// wpf ui helpers methods
    /// </summary>
    public static class UIHelper
    {
        /// <summary>
        /// show an error dialog from an exception
        /// </summary>
        /// <param name="ex">exception origin of the error that must be displayed</param>
        public static void ShowError(Exception ex)
        {
            var messages = new List<string>() { ex.Message };
            var innerException = ex;
            while ((innerException = innerException.InnerException) != null)
            {
                messages.Add(innerException.Message);
            }
            messages.Reverse();
            var message = string.Join(Environment.NewLine, messages);
            _ = MessageBox.Show(message, "error", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
}
