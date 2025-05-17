namespace Rolfin.Mediator.ExternalLibrary;


internal class ExternalEventHandler : IEventHandler<ExternalEvent>
{
    public ValueTask HandleAsync(IEvent @event, CancellationToken cancellationToken = default)
    {
        return ValueTask.CompletedTask;
    }
}


internal class AnotherExternalEventHandler : IEventHandler<ExternalEvent>
{
    public ValueTask HandleAsync(IEvent @event, CancellationToken cancellationToken = default)
    {
        return ValueTask.CompletedTask;
    }
}
