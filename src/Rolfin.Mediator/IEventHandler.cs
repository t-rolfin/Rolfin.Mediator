namespace Rolfin.Mediator;


public interface IEventHandler<in TEvent>  where TEvent : IEvent 
{
    ValueTask HandleAsync(IEvent @event, CancellationToken cancellationToken = default);
}
