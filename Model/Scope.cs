namespace DependencyInjection.Model;

internal class Scope : IScope
{
    private readonly IServiceFactory _serviceFactory;

    public Scope(IDictionary<Type, ServiceDescriptor> descriptorsMap)
    {
        _serviceFactory = new ServiceFactory(descriptorsMap);
    }

    public TService Resolve<TService>() 
        => _serviceFactory.Create<TService>(this);
}