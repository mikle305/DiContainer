using System.Collections.Immutable;

namespace DependencyInjection.Model;

public class Container : IContainer
{
    private readonly IDictionary<Type, ServiceDescriptor> _descriptors;

    public Container(IEnumerable<ServiceDescriptor> services)
    {
        _descriptors = services.ToImmutableDictionary(d => d.ServiceType);
    }

    public IScope CreateScope() 
        => new Scope(_descriptors);
}