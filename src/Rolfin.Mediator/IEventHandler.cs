namespace Rolfin.Mediator;


/// <summary>
/// Defines a handler for a event
/// </summary>
/// <typeparam name="TNotification">The type of command being handled</typeparam>
public interface IEventHandler<in TEvent>  where TEvent : IEvent 
{
    ValueTask HandleAsync(IEvent @event, CancellationToken cancellationToken = default);
}
