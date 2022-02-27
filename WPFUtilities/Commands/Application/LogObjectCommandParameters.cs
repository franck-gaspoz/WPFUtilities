
using System.Windows;

using Microsoft.Extensions.Logging;

using WPFUtilities.Components.Services.Command.Attributes;

namespace WPFUtilities.Commands.Application
{
    /// <summary>
    /// log object command parameters
    /// </summary>
    [ServiceCommandParameters]
    public class LogObjectCommandParameters : DependencyObject
    {
        /// <summary>
        /// Message
        /// </summary>
        public object Message
        {
            get => (object)GetValue(MessageProperty);
            set { SetValue(MessageProperty, value); }
        }

        /// <summary>
        /// message property
        /// </summary>
        public static readonly DependencyProperty MessageProperty =
        DependencyProperty.Register(
            "Message",
            typeof(string),
            typeof(LogObjectCommandParameters),
            new UIPropertyMetadata(null));

        /// <summary>
        /// log level
        /// </summary>
        public LogLevel LogLevel { get; set; } = LogLevel.Information;

        /// <summary>
        /// Data
        /// </summary>
        public object Data
        {
            get => (object)GetValue(DataProperty);
            set { SetValue(DataProperty, value); }
        }

        /// <summary>
        /// data property
        /// </summary>
        public static readonly DependencyProperty DataProperty =
                DependencyProperty.Register(
                    "Data",
                    typeof(object),
                    typeof(LogObjectCommandParameters),
                    new UIPropertyMetadata(null));
    }
}
