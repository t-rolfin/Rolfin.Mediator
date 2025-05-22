namespace Rolfin.Mediator.ExternalLibrary;


internal class ExternalEventHandler : IEventHandler<ExternalEvent>
{
    public Task HandleAsync(IEvent @event, CancellationToken cancellationToken = default)
    {
        return Task.CompletedTask;
    }
}


internal class AnotherExternalEventHandler : IEventHandler<ExternalEvent>
{
    public Task HandleAsync(IEvent @event, CancellationToken cancellationToken = default)
    {
        return Task.CompletedTask;
    }
}
