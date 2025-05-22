namespace Rolfin.Mediator;


/// <summary>
/// Defines a handler for a command without response type
/// </summary>
/// <typeparam name="TNotification">The type of command being handled</typeparam>
public interface ICommandHandler<in TCommand> : ICommand 
{
    public Task HandleAsync(TCommand command, CancellationToken cancellationToken);
}


/// <summary>
/// Defines a handler for a command
/// </summary>
/// <typeparam name="TNotification">The type of command being handled</typeparam>
public interface ICommandHandler<in TCommand, TResponse> where TCommand : ICommand<TResponse>
{
    public Task<TResponse> HandleAsync(TCommand command, CancellationToken cancellationToken);
}
