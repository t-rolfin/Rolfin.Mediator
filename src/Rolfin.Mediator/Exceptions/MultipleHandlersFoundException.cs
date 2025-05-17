namespace Rolfin.Mediator.Exceptions;


[Serializable]
public class MultipleHandlersFoundException : Exception
{
	public MultipleHandlersFoundException() { }
	public MultipleHandlersFoundException(string message) : base(message) { }
	public MultipleHandlersFoundException(string message, Exception inner) : base(message, inner) { }
}
