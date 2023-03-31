using DiContainer.Core.Model.ServicesCreators;

namespace DiContainer.Core.Model
{
    public interface IContainerBuilder
    {
        public void Register(ServiceDescriptor serviceDescriptor);

        public ContainerBuilder WithCustomServiceCreator<TServiceFactory>() where TServiceFactory : ServiceFactory;
    
        public IContainer Build();
    }
}

