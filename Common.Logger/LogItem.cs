using System;

namespace Common.Logger
{
    public class LogItem
    {
        static long _Index = 0;

        public long Index { get; protected set; }

        public DateTime DateTime { get; set; }

        public string Text { get; set; }

        public LogType LogType { get; set; }

        public LogCategory LogCategory { get; set; }

        public string CallerMemberName { get; set; }

        public int CallerLineNumber { get; set; }

        public string CallerFilePath { get; set; }

        public LogItem(
            string text,
            LogType logType = LogType.NotDefined,
            LogCategory logCategory = LogCategory.NotDefined,
            string callerMemberName = "",
            int callerLineNumber = -1,
            string callerFilePath = ""
            )
        {
            Index = _Index;
            _Index++;
            DateTime = DateTime.Now;
            Text = text;
            LogType = logType;
            LogCategory = logCategory;
            CallerMemberName = callerMemberName;
            CallerLineNumber = callerLineNumber;
            CallerFilePath = callerFilePath;
        }

        public override string ToString()
        {
            return (!string.IsNullOrWhiteSpace(Text) ? ($"[{LogType}] ") : "") + Text;
        }
    }
}
