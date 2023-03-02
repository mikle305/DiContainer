namespace DependencyInjection.Model;

internal class InstanceBasedServiceDescriptor : ServiceDescriptor
{
    public object Instance { get; }

    public InstanceBasedServiceDescriptor(Type serviceType, object instance)
    {
        Instance = instance;
        ServiceType = serviceType;
        LifeTime = LifeTime.Single;
    }
}