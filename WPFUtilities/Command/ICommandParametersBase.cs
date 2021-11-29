namespace WPFUtilities.Command
{
    public interface ICommandParametersBase
    {
        object CommandSource { get; set; }
        System.Action<ICommandParametersBase> OnExecuteCallback { get; set; }
        System.Action<ICommandParametersBase> OnExecutedCallback { get; set; }
        System.Func<ICommandParametersBase,bool> CanExecute { get; set; }

        void OnExecute();
        void OnExecuted();
    }
}