using DependencyInjection.Model.Factory;

namespace DependencyInjection.Model;

internal class ContainerProvider : IContainerProvider
{
    private readonly IServiceFactory _serviceFactory;
    private readonly IDictionary<Type, ServiceDescriptor> _descriptors;
    private readonly IScope _rootScope;


    public ContainerProvider(IServiceFactory serviceFactory, IDictionary<Type, ServiceDescriptor> descriptors)
    {
        _serviceFactory = serviceFactory;
        _descriptors = descriptors;
        _rootScope = new Scope(this);
    }

    public object CreateInstance(IScope scope, Type serviceType) 
        => _serviceFactory.Create(scope, serviceType);

    public ServiceDescriptor? GetDescriptor(Type serviceType)
    {
        _descriptors.TryGetValue(serviceType, out ServiceDescriptor? descriptor);
        return descriptor;
    }

    public IScope GetRootScope()
        => _rootScope;
}