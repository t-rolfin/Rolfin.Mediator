namespace Rolfin.Mediator;

public interface IQueryHandler<in TQuery, TResponse> where TQuery : IQuery<TResponse>
{
    public ValueTask<TResponse> QueryAsync(TQuery request, CancellationToken cancellationToken);
}