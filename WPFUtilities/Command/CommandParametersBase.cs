using System;

namespace WPFUtilities.Command
{
    public class CommandParametersBase : ICommandParametersBase
    {
        public DateTime BeginExecutionDateTime { get; set; }
        public DateTime EndExecutionDateTime { get; set; }
        public TimeSpan Duration { get; set; } 

        public object CommandSource { get; set; }

        public Action<ICommandParametersBase> OnExecuteCallback { get; set; }
        public Action<ICommandParametersBase> OnExecutedCallback { get; set; }
        public Func<ICommandParametersBase,bool> CanExecute { get; set; }

        public CommandParametersBase()
        {

        }

        public void OnExecutionEnd()
        {
            EndExecutionDateTime = DateTime.Now;
            Duration = EndExecutionDateTime - BeginExecutionDateTime;
        }

        public void OnExecute()
        {
            BeginExecutionDateTime = DateTime.Now;
            OnExecuteCallback?.Invoke(this);
        }

        public void OnExecuted()
        {
            if (Duration == null)
                OnExecutionEnd();
                
            OnExecutedCallback?.Invoke(this);
        }

    }
}
