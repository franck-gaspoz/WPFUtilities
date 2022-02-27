
using System.Text;

using Microsoft.Extensions.Logging;

using WPFUtilities.Commands.Abstract;
using WPFUtilities.Components.ServiceComponent;
using WPFUtilities.Components.Services.Command;

namespace WPFUtilities.Commands.Application
{
    /// <summary>
    /// detailled log command
    /// </summary>
    public class LogObjectCommand :
        AbstractServiceCommand<
            LogObjectCommand,
            string,
            object,
            LogLevel
            >
    {
        readonly ILogger<LogObjectCommand> _logger;

        /// <inheritdoc/>
        public LogObjectCommand(IServiceComponentProvider serviceProvider,
            ILogger<LogObjectCommand> logger)
            : base(serviceProvider)
        {
            _logger = logger;
        }

        /// <summary>
        /// log a message with details
        /// </summary>
        /// <param name="context">execute context</param>
        /// <param name="message">log message</param>
        /// <param name="data">additional data (optional, default null)</param>
        /// <param name="logLevel">log level (default Information)</param>
        public override void Execute(
            IServiceCommandExecuteContext context,
            string message,
            object data = null,
            LogLevel logLevel = LogLevel.Information
            )
        {
            _logger.Log(logLevel, message);
            if (data != null)
                _logger.Log(logLevel, ObjectPropertiesDump(data));
        }

        static string ObjectPropertiesDump(object data, string leftMargin = "", string padLeft = "    ")
        {
            if (data == null) return StringValue(data);
            var sb = new StringBuilder();
            sb.AppendLine(leftMargin + "{");
            foreach (var propInfo in data.GetType().GetProperties())
            {
                sb.AppendLine(leftMargin + padLeft + $"{propInfo.Name} ({propInfo.PropertyType.Name}) = {StringValue(propInfo.GetValue(data))}");
            }
            sb.AppendLine(leftMargin + "}");
            return sb.ToString();
        }

        static string StringValue(object value)
            => (value == null) ? "null"
                : (value is string str) ?
                    $"\"{str}\""
                    : value.ToString();
    }
}

