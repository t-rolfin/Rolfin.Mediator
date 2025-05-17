using System.Reflection;


namespace Rolfin.Mediator;

public record MediatorServiceConfiguration
{
    /// <summary>
    /// Service lifetime to register services under. Default value is <see cref="ServiceLifetime.Transient"/>
    /// </summary>
    public ServiceLifetime Lifetime { get; set; } = ServiceLifetime.Transient;


    /// <summary>
    /// Register various handlers from assembly
    /// </summary>
    /// <param name="assembly">Assembly to scan</param>
    /// <returns>This</returns>
    public MediatorServiceConfiguration RegisterServicesFromAssembly(Assembly assembly)
    {
        AssembliesToRegister.Add(assembly);
        return this;
    }


    /// <summary>
    /// Register various handlers from assemblies
    /// </summary>
    /// <param name="assemblies">Assemblies to scan</param>
    /// <returns>This</returns>
    public MediatorServiceConfiguration RegisterServicesFromAssemblies(params Assembly[] assemblies)
    {
        AssembliesToRegister.AddRange(assemblies);
        return this;
    }


    internal List<Assembly> AssembliesToRegister { get; } = [];
}
