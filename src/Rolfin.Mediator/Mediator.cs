namespace Rolfin.Mediator;


public class Mediator : IMediator
{
    IServiceProvider _serviceProvider;


    public Mediator(IServiceProvider serviceProvider) 
        => _serviceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));


    public async ValueTask<TResponse> Send<TResponse>(ICommand<TResponse> command, CancellationToken cancellationToken = default)
    {
        var handlerType = typeof(ICommandHandler<,>).MakeGenericType(command.GetType(), typeof(TResponse));
        var handler = _serviceProvider.GetService(handlerType);

        if (handler is null) throw new InvalidOperationException($"No handler was registered for command type {command.GetType().Name}");

        var methodInfo = handler.GetType().GetMethod(nameof(ICommandHandler<ICommand<TResponse>, TResponse>.HandleAsync));
        if (methodInfo is null) throw new InvalidOperationException(
                $"Handler for {command.GetType().Name} does not have the expected " +
                $"{nameof(ICommandHandler<ICommand<TResponse>, TResponse>.HandleAsync)} method signiture."
            );

        var task = (Task<TResponse>?)methodInfo.Invoke(handler, [ command, cancellationToken ]);
        return await task.ConfigureAwait(false);
    }
    public async ValueTask<TResponse> Send<TResponse>(IQuery<TResponse> query, CancellationToken cancellationToken = default)
    {
        var handlerType = typeof(IQueryHandler<,>).MakeGenericType(query.GetType(), typeof(TResponse));
        var handler = _serviceProvider.GetService(handlerType);

        if (handler is null) throw new InvalidOperationException($"No handler was registered for query type {query.GetType().Name}");

        var methodInfo = handler.GetType().GetMethod(nameof(IQueryHandler<IQuery<TResponse>, TResponse>.QueryAsync));
        if (methodInfo is null) throw new InvalidOperationException(
                $"Handler for {query.GetType().Name} does not have the expected " +
                $"{nameof(IQueryHandler<IQuery<TResponse>, TResponse>.QueryAsync)} method signiture."
            );

        var task = (Task<TResponse>?)methodInfo.Invoke(handler, [query, cancellationToken]);
        return await task.ConfigureAwait(false);
    }
    public async ValueTask Send(ICommand command, CancellationToken cancellationToken = default)
    {
        var handlerType = typeof(ICommandHandler<>).MakeGenericType(command.GetType());
        var handler = _serviceProvider.GetService(handlerType);

        if (handler is null) throw new InvalidOperationException($"No handler was registered for command type {command.GetType().Name}");

        var methodInfo = handler.GetType().GetMethod(nameof(ICommandHandler<ICommand>.HandleAsync));
        if (methodInfo is null) throw new InvalidOperationException(
                $"Handler for {command.GetType().Name} does not have the expected " +
                $"{nameof(ICommandHandler<ICommand>.HandleAsync)} method signiture."
            );

        var task = (ValueTask)methodInfo.Invoke(handler, [ cancellationToken ])!;
        await task.ConfigureAwait(false);
    }
}
