namespace Rolfin.Mediator.Tests.unts;


public record RequestWithParams(int param) : ICommand;


public record RequestWithParamsAndResponse(int param1, int param2) : ICommand<object[]>;

internal class CreateHandler : ICommandHandler<RequestWithParams>
{
    public Task HandleAsync(RequestWithParams command, CancellationToken cancellationToken)
        => Task.CompletedTask;
}


internal class CreateWithResponseHandler : ICommandHandler<RequestWithParamsAndResponse, object[]>
{
    public Task<object[]> HandleAsync(RequestWithParamsAndResponse command, CancellationToken cancellationToken)
        => Task.FromResult<object[]>([]);
}


internal class CreateWithResponseSecHandler : ICommandHandler<RequestWithParamsAndResponse, object[]>
{
    public Task<object[]> HandleAsync(RequestWithParamsAndResponse command, CancellationToken cancellationToken)
        => Task.FromResult<object[]>([]);
}