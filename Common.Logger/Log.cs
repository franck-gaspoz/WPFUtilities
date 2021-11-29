using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Common.Logger
{
    public static class Log
    {
        public static ILog LogImpl { get; set; }
            = new LogBase();

        public static bool IsRecordingEnabled
        {
            get
            {
                return LogImpl.IsRecordingEnabled;
            }
            set
            {
                LogImpl.IsRecordingEnabled = true;
            }
        }

        public static BindingList<LogItem> LogItems
        {
            get
            {
                return LogImpl.LogItems;
            }
        }

        public static Action<BindingList<LogItem>, LogItem> AddAction
        {
            get
            {
                return LogImpl.AddAction;
            }
            set
            {
                LogImpl.AddAction = value;
            }
        }

        public static void Clear()
        {
            LogItems.Clear();
        }

        public static void Info(
            string text,
            LogCategory logCategory = LogCategory.NotDefined,
            [CallerMemberName] string callerMemberName = "",
            [CallerLineNumber] int callerLineNumber = 0,
            [CallerFilePath] string callerFilePath = ""
            )
        {
            LogImpl.Info(
                text,
                logCategory,
                callerMemberName,
                callerLineNumber,
                callerFilePath
                );
        }

        public static void Error(
            string text,
            LogCategory logCategory = LogCategory.NotDefined,
            [CallerMemberName] string callerMemberName = "",
            [CallerLineNumber] int callerLineNumber = 0,
            [CallerFilePath] string callerFilePath = ""
            )
        {
            LogImpl.Error(
                text,
                logCategory,
                callerMemberName,
                callerLineNumber,
                callerFilePath
                );
        }

        public static void Warning(
            string text,
            LogCategory logCategory = LogCategory.NotDefined,
            [CallerMemberName] string callerMemberName = "",
            [CallerLineNumber] int callerLineNumber = 0,
            [CallerFilePath] string callerFilePath = ""
            )
        {
            LogImpl.Warning(
                text,
                logCategory,
                callerMemberName,
                callerLineNumber,
                callerFilePath
                );
        }

        public static void Debug(
            string text,
            LogCategory logCategory = LogCategory.NotDefined,
            [CallerMemberName] string callerMemberName = "",
            [CallerLineNumber] int callerLineNumber = 0,
            [CallerFilePath] string callerFilePath = ""
            )
        {
            LogImpl.Debug(
                text,
                logCategory,
                callerMemberName,
                callerLineNumber,
                callerFilePath
                );
        }

        public static void Add(
            string text,
            LogType logType = LogType.NotDefined,
            LogCategory logCategory = LogCategory.NotDefined,
            [CallerMemberName] string callerMemberName = "",
            [CallerLineNumber] int callerLineNumber = 0,
            [CallerFilePath] string callerFilePath = ""
            )
        {
            LogImpl.Add(
                text,
                logType,
                logCategory,
                callerMemberName,
                callerLineNumber,
                callerFilePath
                );
        }
    }
}
