namespace Rolfin.Mediator.Exceptions;


[Serializable]
public class NotHandlerFoundException : Exception
{
	public NotHandlerFoundException() { }
	public NotHandlerFoundException(string message) : base(message) { }
	public NotHandlerFoundException(string message, Exception inner) : base(message, inner) { }
}
