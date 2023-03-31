using UniDependencyInjection.Core.Model.Descriptors;
using UniDependencyInjection.Core.Model.ServiceCreators;

namespace UniDependencyInjection.Core.Model
{
    public interface IContainerBuilder
    {
        public void Register(ServiceDescriptor serviceDescriptor);

        public ContainerBuilder WithCustomServiceCreator<TServiceFactory>() where TServiceFactory : ServiceFactory;
    
        public IContainer Build();
    }
}

