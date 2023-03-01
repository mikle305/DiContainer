namespace DependencyInjection.Model;

public abstract class ServiceDescriptor
{
    public Type ServiceType { get; init; }

    public LifeTime LifeTime { get; init; }
}