namespace DependencyInjection.Model;

internal class ContainerProvider : IContainerProvider
{
    private readonly IServiceFactory _serviceFactory;
    private readonly IDictionary<Type, ServiceDescriptor> _descriptors;


    public ContainerProvider(IServiceFactory serviceFactory, IDictionary<Type, ServiceDescriptor> descriptors)
    {
        _serviceFactory = serviceFactory;
        _descriptors = descriptors;
    }

    public TService CreateInstance<TService>(IScope scope)
    {
        return _serviceFactory.Create<TService>(scope);
    }

    public ServiceDescriptor? GetDescriptor<TService>()
    {
        _descriptors.TryGetValue(typeof(TService), out ServiceDescriptor? descriptor);
        return descriptor;
    }
}