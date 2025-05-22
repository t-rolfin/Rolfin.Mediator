namespace Rolfin.Mediator;


/// <summary>
/// Defines a handler for a query
/// </summary>
/// <typeparam name="TResponse">The type of query being handled</typeparam>
public interface IQueryHandler<in TQuery, TResponse> where TQuery : IQuery<TResponse>
{
    public Task<TResponse> QueryAsync(TQuery request, CancellationToken cancellationToken);
}