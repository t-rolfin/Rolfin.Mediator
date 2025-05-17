namespace Rolfin.Mediator.ExternalLibrary;


public record ExternalEvent(string eventName = "primary") : IEvent;
