using DependencyInjection.Model.Factory;

namespace DependencyInjection.Model;

public interface IContainerBuilder
{
    public void Register(ServiceDescriptor serviceDescriptor);

    public ContainerBuilder WithCustomFactory<TServiceFactory>() where TServiceFactory : ServiceFactory;
    
    public IContainer Build();
}