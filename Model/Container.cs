using System.Collections.Immutable;
using DependencyInjection.Model.Factory;

namespace DependencyInjection.Model;

public class Container : IContainer
{
    private readonly IContainerProvider _containerProvider;

    
    internal Container(IEnumerable<ServiceDescriptor> services)
    {
        IDictionary<Type, ServiceDescriptor> descriptors = 
            services.ToImmutableDictionary(d => d.ServiceType);

        var serviceFactory = new ReflectionServiceFactory(descriptors);
        
        _containerProvider = new ContainerProvider(serviceFactory, descriptors);
    }

    public IScope CreateScope()
        => new Scope(_containerProvider);

    public void Dispose() 
        => _containerProvider.GetRootScope().Dispose();

    public ValueTask DisposeAsync() 
        => _containerProvider.GetRootScope().DisposeAsync();
}