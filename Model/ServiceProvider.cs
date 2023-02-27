namespace DependencyInjection.Model;

public class ServiceProvider : IServiceProvider
{
    private Dictionary<Type, ServiceDescriptor> _descriptors = new();

    public ServiceProvider(IEnumerable<ServiceDescriptor> services)
    {
        _descriptors = services.ToDictionary(d => d.ServiceType);
    }

    public IScope CreateScope()
        => new Scope(_descriptors);
}