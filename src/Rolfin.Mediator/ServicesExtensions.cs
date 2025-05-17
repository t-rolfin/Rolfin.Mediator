namespace Rolfin.Mediator;


public static class ServicesExtensions
{
    public static IServiceCollection AddMediator(this IServiceCollection services, params Type[] handlerAssemblyMarkers)
    {
        services.AddScoped<IMediator, Mediator>();

        foreach (var marker in handlerAssemblyMarkers)
            services.Scan(scan => scan
                .FromAssembliesOf(marker)
                .AddClasses(classes => classes.AssignableTo(typeof(ICommandHandler<,>)), publicOnly: false)
                    .AsImplementedInterfaces()
                .AddClasses(classes => classes.AssignableTo(typeof(IQueryHandler<,>)), publicOnly: false)
                    .AsImplementedInterfaces()
                .AddClasses(classes => classes.AssignableTo(typeof(ICommandHandler<>)), publicOnly: false)
                    .AsImplementedInterfaces()
                    .WithScopedLifetime()
                );

        return services;
    }
}
