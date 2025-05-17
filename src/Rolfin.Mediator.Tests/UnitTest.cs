using Microsoft.Extensions.DependencyInjection;
using Rolfin.Mediator.Tests.unts;


namespace Rolfin.Mediator.Tests
{
    public class UnitTest
    {
        [Fact]
        public async Task success_register_dependency()
        {
            var container = new ServiceCollection();
            container.AddMediator(typeof(UnitTest));

            var serviceProvider = container.BuildServiceProvider();

            var mediator = serviceProvider.GetRequiredService<IMediator>();

            var args = new RequestWithParamsAndResponse(1, 2);
            var ss = await mediator.Send(args);
        }
    }
}