namespace DependencyInjection.Model;

public class ContainerBuilder : IContainerBuilder
{
    private List<ServiceDescriptor> _services = new();

    
    public void Register(ServiceDescriptor serviceDescriptor)
    {
        _services.Add(serviceDescriptor);
    }

    public IContainer Build()
    {
        return new Container(_services);
    }
}