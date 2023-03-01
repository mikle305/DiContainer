using System.Collections.Immutable;

namespace DependencyInjection.Model;

public class Container : IContainer
{
    private readonly IContainerProvider _containerProvider;

    
    public Container(IEnumerable<ServiceDescriptor> services)
    {
        IDictionary<Type, ServiceDescriptor> descriptors = services.ToImmutableDictionary(d => d.ServiceType);
        IServiceFactory serviceFactory = new ServiceFactory(descriptors);
        
        _containerProvider = new ContainerProvider(serviceFactory, descriptors);
    }

    public IScope CreateScope()
        => new Scope(_containerProvider);
}