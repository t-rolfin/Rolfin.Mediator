using Microsoft.Extensions.DependencyInjection;


namespace Rolfin.Mediator.Tests
{
    public class UnitTest
    {
        [Fact]
        public void success_register_dependency()
        {
            var container = new ServiceCollection();
            container.AddMediator(typeof(UnitTest));

            var serviceProvider = container.BuildServiceProvider();

            var mediator = serviceProvider.GetService<IMediator>();
            Assert.NotNull(mediator);
        }
    }
}