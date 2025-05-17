namespace Rolfin.Mediator;


/// <summary>
/// Marker interface that represents a command
/// </summary>
/// <typeparam name="TResponse">Response Type</typeparam>
public interface ICommand<out TResponse>;


/// <summary>
/// Marker interface that represents a command without response type
/// </summary>
public interface ICommand;