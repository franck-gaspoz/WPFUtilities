using System;
using System.Windows;
using System.Windows.Input;

using WPFUtilities.Components;

using static Common.Logger.Log;

namespace WPFUtilities.Command
{
    public abstract class CommandBase<T> :
        Dispatchable,
        ICommand
    {
        public Action OnCompletedCallback;

        public CommandParametersBase DefaultParameters = new CommandParametersBase();

        public string Error;

        /// <summary>
        /// agnostic constructor
        /// <para>parameters provided on command binding</para>
        /// </summary>
        public CommandBase() : base() { }

        public T Cast(object parameter)
        {
            if (parameter == null)
                return default(T);
            var p = CheckParamIs<T>(parameter, parameter.GetType().Name.Replace("CommandParamters", ""));
            return p;
        }

        public P CheckParamIs<P>(object parameter, string cmdName)
        {
            var p = (P)parameter;
            if (p == null)
                Error($"command '{nameof(cmdName)}' parameter is null or wrong type: {parameter}. Attempted type is '{typeof(T).Name}'");
            return p;
        }

        public void OnCompleted()
        {
            OnCompletedCallback?.Invoke();
        }

        public virtual bool CanExecute(object parameter)
        {
            //if (DesignerProperties.IsInDesignModeProperty)
            if (CurrentApp == null) return false;
            if (parameter is ICommandParametersBase p && p.CanExecute != null)
                return p.CanExecute(p);
            return Application.Current != null && Application.Current.MainWindow != null && !CurrentApp.IsBuzy;
        }

        public virtual event EventHandler CanExecuteChanged
        {
            // You may not need a body here at all...
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public abstract void Execute(object parameter);

        public R Run<R>(Func<R> func)
        {
            try
            {
                return func();
            }
            catch (Exception ex)
            {
                var m = ex + "";
                MessageBox.Show(m, "error", MessageBoxButton.OK, MessageBoxImage.Error);
                return default;
            }
        }

        public void Run(Action action)
            => Run<object>(() => { action(); return null; });
    }
}
