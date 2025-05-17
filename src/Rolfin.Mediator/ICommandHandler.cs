namespace Rolfin.Mediator;


public interface ICommandHandler<in TCommand> : ICommand 
{
    public ValueTask HandleAsync(TCommand command, CancellationToken cancellationToken);
}


public interface ICommandHandler<in TCommand, TResponse> where TCommand : ICommand<TResponse>
{
    public ValueTask<TResponse> HandleAsync(TCommand command, CancellationToken cancellationToken);
}
