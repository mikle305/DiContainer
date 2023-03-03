using DependencyInjection.Helpers;
using DependencyInjection.Model.Factory;

namespace DependencyInjection.Model;

public class ContainerBuilder : IContainerBuilder
{
    private readonly List<ServiceDescriptor> _services = new();
    private Type? _serviceFactory;


    public void Register(ServiceDescriptor serviceDescriptor)
    {
        _services.Add(serviceDescriptor);
    }

    public ContainerBuilder WithCustomFactory<TServiceFactory>() where TServiceFactory : ServiceFactory
    {
        if (_serviceFactory is not null)
            ExceptionsHelper.ThrowServiceFactoryAlreadyAdded();
            
        _serviceFactory = typeof(TServiceFactory);
        return this;
    }

    public IContainer Build()
    {
        return _serviceFactory is not null 
            ? new Container(_services, _serviceFactory) 
            : new Container(_services);
    }
}