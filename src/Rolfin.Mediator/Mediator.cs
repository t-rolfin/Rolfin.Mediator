using Rolfin.Mediator.Exceptions;


namespace Rolfin.Mediator;


public class Mediator : IMediator
{
    IServiceProvider _serviceProvider;


    public Mediator(IServiceProvider serviceProvider) 
        => _serviceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));


    public async ValueTask PublishAsync(IEvent @event, CancellationToken cancellationToken = default)
    {
        string commandName = @event.GetType().Name;

        var handlerType = typeof(IEventHandler<>).MakeGenericType(@event.GetType());
        var handlers = _serviceProvider.GetServices(handlerType);
        if (handlers is null || handlers.Any() is false) throw new NotHandlerFoundException($"Couldn't be found any handlers for \"{commandName}\"");

        foreach(var handler in handlers)
        {
            if (handler is null) continue;

            var methodInfo = handler.GetType().GetMethod(nameof(IEventHandler<IEvent>.HandleAsync));

            if (methodInfo is null) 
                throw new InvalidOperationException(
                    $"Handler for {commandName} does not have the expected " +
                    $"{nameof(IEventHandler<IEvent>.HandleAsync)} method signiture."
                );

            var task = (ValueTask)methodInfo.Invoke(handler, [ @event, cancellationToken ])!;
            await task.ConfigureAwait(false);
        }
    }
    public async ValueTask SendAsync(ICommand command, CancellationToken cancellationToken = default)
    {
        string commandName = command.GetType().Name;

        var handlerType = typeof(ICommandHandler<>).MakeGenericType(command.GetType());
        var handlers = _serviceProvider.GetServices(handlerType);

        if (handlers.Count() > 1) throw new MultipleHandlersFoundException($"Where found more then one registered handler of type \"{commandName}\"");
        if (handlers is null || handlers.Any() is false) throw new NotHandlerFoundException($"Couldn't be found any handlers for \"{commandName}\"");

        var handler = handlers.First();
        var methodInfo = handler!.GetType().GetMethod(nameof(ICommandHandler<ICommand>.HandleAsync));
        if (methodInfo is null) throw new InvalidOperationException(
                $"Handler for {command.GetType().Name} does not have the expected " +
                $"{nameof(ICommandHandler<ICommand>.HandleAsync)} method signiture."
            );

        var task = (ValueTask)methodInfo.Invoke(handler, [ command, cancellationToken ])!;
        await task.ConfigureAwait(false);
    }
    public async ValueTask<TResponse> SendAsync<TResponse>(IQuery<TResponse> query, CancellationToken cancellationToken = default)
    {
        string queryName = query.GetType().Name;

        var handlerType = typeof(IQueryHandler<,>).MakeGenericType(query.GetType(), typeof(TResponse));
        var handlers = _serviceProvider.GetServices(handlerType);

        if (handlers.Count() > 1) throw new MultipleHandlersFoundException($"Where found more then one registered handler of type \"{queryName}\"");
        if (handlers is null || handlers.Any() is false) throw new NotHandlerFoundException($"Couldn't be found any handlers for \"{queryName}\"");

        var handler = handlers.First();
        var methodInfo = handler!.GetType().GetMethod(nameof(IQueryHandler<IQuery<TResponse>, TResponse>.QueryAsync));
        if (methodInfo is null) throw new InvalidOperationException(
                $"Handler for {query.GetType().Name} does not have the expected " +
                $"{nameof(IQueryHandler<IQuery<TResponse>, TResponse>.QueryAsync)} method signiture."
            );

        var task = (Task<TResponse>?)methodInfo.Invoke(handler, [query, cancellationToken]);
        return await task.ConfigureAwait(false);
    }
    public async ValueTask<TResponse> SendAsync<TResponse>(ICommand<TResponse> command, CancellationToken cancellationToken = default)
    {
        string commandName = command.GetType().Name;

        var handlerType = typeof(ICommandHandler<,>).MakeGenericType(command.GetType(), typeof(TResponse));
        var handlers = _serviceProvider.GetServices(handlerType);

        if (handlers.Count() > 1) throw new MultipleHandlersFoundException($"Where found more then one registered handler of type \"{commandName}\"");
        if (handlers is null || handlers.Any() is false) throw new NotHandlerFoundException($"Couldn't be found any handlers for \"{commandName}\"");  

        var handler = handlers.First();
        var methodInfo = handler!.GetType().GetMethod(nameof(ICommandHandler<ICommand<TResponse>, TResponse>.HandleAsync));
        if (methodInfo is null) throw new InvalidOperationException(
                $"Handler for {commandName} does not have the expected " +
                $"{nameof(ICommandHandler<ICommand<TResponse>, TResponse>.HandleAsync)} method signiture."
            );

        var task = (Task<TResponse>?)methodInfo.Invoke(handler, [ command, cancellationToken ]);
        return await task.ConfigureAwait(false);
    }
}
