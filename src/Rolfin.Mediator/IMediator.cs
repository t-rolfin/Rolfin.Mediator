namespace Rolfin.Mediator;

public interface IMediator
{
    ValueTask<TResponse> Send<TResponse>(ICommand<TResponse> command, CancellationToken cancellationToken = default);
    ValueTask<TResponse> Send<TResponse>(IQuery<TResponse> query, CancellationToken cancellationToken = default);
    ValueTask Send(ICommand command, CancellationToken cancellationToken = default);
}
