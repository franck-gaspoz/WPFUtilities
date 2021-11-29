using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace Common.Logger
{
    public class LogBase
        : ILog
    {
        public Action<BindingList<LogItem>, LogItem> AddAction { get; set; } = null;

        public bool IsRecordingEnabled { get; set; } = true;

        public BindingList<LogItem> LogItems { get; } =
            new BindingList<LogItem>();

        public void Info(
            string text,
            LogCategory logCategory = LogCategory.NotDefined,
            [CallerMemberName] string callerMemberName = "",
            [CallerLineNumber] int callerLineNumber = 0,
            [CallerFilePath] string callerFilePath = ""
            )
        {
            Add(text, LogType.Info, logCategory, callerMemberName, callerLineNumber, callerFilePath);
        }

        public void Error(
            string text,
            LogCategory logCategory = LogCategory.NotDefined,
            [CallerMemberName] string callerMemberName = "",
            [CallerLineNumber] int callerLineNumber = 0,
            [CallerFilePath] string callerFilePath = ""
            )
        {
            Add(text, LogType.Error, logCategory, callerMemberName, callerLineNumber, callerFilePath);
        }

        public void Warning(
            string text,
            LogCategory logCategory = LogCategory.NotDefined,
            [CallerMemberName] string callerMemberName = "",
            [CallerLineNumber] int callerLineNumber = 0,
            [CallerFilePath] string callerFilePath = ""
            )
        {
            Add(text, LogType.Warning, logCategory, callerMemberName, callerLineNumber, callerFilePath);
        }

        public void Debug(
            string text,
            LogCategory logCategory = LogCategory.NotDefined,
            [CallerMemberName] string callerMemberName = "",
            [CallerLineNumber] int callerLineNumber = 0,
            [CallerFilePath] string callerFilePath = ""
            )
        {
            Add(text, LogType.Debug, logCategory, callerMemberName, callerLineNumber, callerFilePath);
        }

        public void Add(
            string text,
            LogType logType = LogType.NotDefined,
            LogCategory logCategory = LogCategory.NotDefined,
            [CallerMemberName] string callerMemberName = "",
            [CallerLineNumber] int callerLineNumber = 0,
            [CallerFilePath] string callerFilePath = ""
            )
        {
            System.Diagnostics.Debug.WriteLine($"-({Process.GetCurrentProcess().ProcessName}) {text}");

            if (IsRecordingEnabled)
            {
                var it =
                    new LogItem(
                        text,
                        logType,
                        logCategory,
                        callerMemberName,
                        callerLineNumber,
                        callerFilePath
                        );
                if (AddAction != null)
                    AddAction(LogItems, it);
                else
                    LogItems.Add(it);
            }
        }
    }
}
