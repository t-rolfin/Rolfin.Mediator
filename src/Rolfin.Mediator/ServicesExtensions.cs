using System.Reflection;


namespace Rolfin.Mediator;


public static class ServicesExtensions
{
    static MediatorServiceConfiguration _configs = new();


    public static IServiceCollection AddMediator(this IServiceCollection services, Action<MediatorServiceConfiguration> configration)
    {
        configration.Invoke(_configs);

        _ = _configs.Lifetime switch
        {
            ServiceLifetime.Scoped => services.AddSingleton<IMediator, Mediator>(),
            ServiceLifetime.Singleton => services.AddSingleton<IMediator, Mediator>(),
            ServiceLifetime.Transient => services.AddTransient<IMediator, Mediator>(),
            _ => services.AddTransient<IMediator, Mediator>()
        };

        services._registerFromAssemblies(_configs.AssembliesToRegister);

        return services;
    }


    static IServiceCollection _registerFromAssemblies(this IServiceCollection services, List<Assembly> assemblies)
        => services.Scan(scan => scan.FromAssemblies(assemblies)
            .AddClasses(classes => classes.AssignableTo(typeof(ICommandHandler<,>)), publicOnly: false)
                .AsImplementedInterfaces()
            .AddClasses(classes => classes.AssignableTo(typeof(IQueryHandler<,>)), publicOnly: false)
                .AsImplementedInterfaces()
            .AddClasses(classes => classes.AssignableTo(typeof(ICommandHandler<>)), publicOnly: false)
                .AsImplementedInterfaces()
            .AddClasses(classes => classes.AssignableTo(typeof(IEventHandler<>)), publicOnly: false)
                .AsImplementedInterfaces()
                .WithScopedLifetime()
        );
}