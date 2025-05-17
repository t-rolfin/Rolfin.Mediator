namespace Rolfin.Mediator;

public interface IMediator
{
    ValueTask<TResponse> SendAsync<TResponse>(ICommand<TResponse> command, CancellationToken cancellationToken = default);
    ValueTask<TResponse> SendAsync<TResponse>(IQuery<TResponse> query, CancellationToken cancellationToken = default);
    ValueTask SendAsync(ICommand command, CancellationToken cancellationToken = default);
    ValueTask PublishAsync(IEvent @event, CancellationToken cancellationToken = default);
}
