using System;
using DiContainer.UniDependencyInjection.Core.Model.ServiceActivator;
using UniDependencyInjection.Core.Model.Descriptors;

namespace UniDependencyInjection.Core.Model
{
    public interface IContainerBuilder
    {
        public IContainerBuilder RegisterTypeBased(
            Type serviceType,
            Type serviceImplementation,
            LifeTime lifeTime);

        public IContainerBuilder RegisterFactoryBased(
            Type serviceType, 
            Func<IScope, object> factory, 
            LifeTime lifeTime);

        public IContainerBuilder RegisterInstanceBased(
            Type serviceType, 
            object instance);

        public IContainerBuilder WithCustomServiceActivatorFactory<TServiceFactory>() 
            where TServiceFactory : ServiceFactory;

        public IContainer Build();
    }
}

