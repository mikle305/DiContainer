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

    public object CreateInstance<TService>(IScope scope) 
        => _serviceFactory.Create<TService>(scope);

    public ServiceDescriptor? GetDescriptor<TService>()
    {
        _descriptors.TryGetValue(typeof(TService), out ServiceDescriptor? descriptor);
        return descriptor;
    }

    public IScope GetRootScope()
        => _rootScope;
}