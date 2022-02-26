using System;
using System.Windows.Input;

namespace WPFUtilities.Components.Services.Command
{
    /// <summary>
    /// relay a IServiceCommand excution to a command service
    /// <para>statefull, unique for a source</para>
    /// </summary>
    public class RelayServiceCommand
        : ICommand
    {
        /// <inheritdoc/>
        public event EventHandler CanExecuteChanged;

        IServiceCommandExecuteContext context;

        WeakReference<IServiceCommand> _command;

        /// <summary>
        /// creates a new instance
        /// </summary>
        /// <param name="command">relayed commands</param>
        /// <param name="context">service command execute context</param>
        /// <exception cref="InvalidOperationException">command can't be null</exception>
        public RelayServiceCommand(
            IServiceCommand command,
            IServiceCommandExecuteContext context)
        {
            if (command == null) throw new InvalidOperationException("command can't be null");
            _command = new WeakReference<IServiceCommand>(command);
        }

        IServiceCommand _serviceCommand =>
            (_command != null && _command.TryGetTarget(out var target))
                ? target : null;

        /// <inheritdoc/>
        public bool CanExecute(object parameter)
            => (bool)_serviceCommand?.CanExecute(parameter);

        /// <inheritdoc/>
        public void Execute(object parameter)
            => _serviceCommand?.Execute(parameter, context);
    }
}
