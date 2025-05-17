using Microsoft.Extensions.DependencyInjection;
using System.Reflection;


namespace Rolfin.Mediator.Tests
{
    public class UnitTest
    {
        Assembly[] _assemblies = [ typeof(UnitTest).Assembly, typeof(ExternalLibrary.Program).Assembly ];
        IServiceProvider _serviceProvider;


        public UnitTest()
        {
            var container = new ServiceCollection();

            container.AddMediator(cfg =>
            {
                cfg.RegisterServicesFromAssemblies(_assemblies);
                cfg.Lifetime = ServiceLifetime.Scoped;
            });

            _serviceProvider = container.BuildServiceProvider();
        }


        [Fact]
        public void success_register_dependency()
        {
            // Arrange
            var mediator = _serviceProvider.GetService<IMediator>();

            // Assert
            Assert.NotNull(mediator);
        }

        [Fact]
        public async Task success_call_external_event_handler()
        {
            // Arrange
            var mediator = _serviceProvider.GetRequiredService<IMediator>();
            var args = new ExternalLibrary.ExternalEvent();

            // Act & Assert
            await mediator.PublishAsync(args);
        }
    }
}