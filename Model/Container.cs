namespace DependencyInjection.Model;

public class Container : IContainer
{
    private readonly Dictionary<Type, ServiceDescriptor> _descriptors;

    public Container(IEnumerable<ServiceDescriptor> services)
    {
        _descriptors = services.ToDictionary(d => d.ServiceType);
    }

    public IScope CreateScope()
        => new Scope(_descriptors);
}