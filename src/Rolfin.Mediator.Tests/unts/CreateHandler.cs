namespace Rolfin.Mediator.Tests.unts;


public record RequestWithParams(int param) : ICommand;


public record RequestWithParamsAndResponse(int param1, int param2) : ICommand<object[]>;

internal class CreateHandler : ICommandHandler<RequestWithParams>
{
    public ValueTask HandleAsync(RequestWithParams command, CancellationToken cancellationToken)
        => ValueTask.CompletedTask;
}


internal class CreateWithResponseHandler : ICommandHandler<RequestWithParamsAndResponse, object[]>
{
    public ValueTask<object[]> HandleAsync(RequestWithParamsAndResponse command, CancellationToken cancellationToken)
        => ValueTask.FromResult<object[]>([]);
}


internal class CreateWithResponseSecHandler : ICommandHandler<RequestWithParamsAndResponse, object[]>
{
    public ValueTask<object[]> HandleAsync(RequestWithParamsAndResponse command, CancellationToken cancellationToken)
        => ValueTask.FromResult<object[]>([]);
}