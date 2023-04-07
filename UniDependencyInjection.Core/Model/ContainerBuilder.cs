using System;
using System.Collections.Generic;
using UniDependencyInjection.Core.Helpers;
using UniDependencyInjection.Core.Model.Descriptors;
using UniDependencyInjection.Core.Model.ServiceCreators;

namespace UniDependencyInjection.Core.Model
{
    public class ContainerBuilder : IContainerBuilder
    {
        private readonly List<ServiceDescriptor> _services = new();
        private Type _serviceFactory;


        public IContainerBuilder RegisterTypeBased(
            Type serviceType,
            Type serviceImplementation,
            LifeTime lifeTime)
        {
            RegisterDescriptor(new TypeBasedServiceDescriptor(serviceType, lifeTime, serviceImplementation));
            return this;
        }

        public IContainerBuilder RegisterFactoryBased(
            Type serviceType, 
            Func<IScope, object> factory, 
            LifeTime lifeTime)
        {
            RegisterDescriptor(new FactoryBasedServiceDescriptor(serviceType, lifeTime, factory));
            return this;
        }

        public IContainerBuilder RegisterInstanceBased(
            Type serviceType, 
            object instance)
        {
            RegisterDescriptor(new InstanceBasedServiceDescriptor(serviceType, instance));
            return this;
        }

        public IContainerBuilder WithCustomServiceActivatorFactory<TServiceFactory>() where TServiceFactory : ServiceFactory
        {
            if (_serviceFactory is not null)
                ExceptionsHelper.ThrowServiceFactoryAlreadyExists();
            
            _serviceFactory = typeof(TServiceFactory);
            return this;
        }

        public IContainer Build()
        {
            return _serviceFactory is not null 
                ? new Container(_services, _serviceFactory) 
                : new Container(_services);
        }

        private void RegisterDescriptor(ServiceDescriptor serviceDescriptor)
        {
            _services.Add(serviceDescriptor);
        }
    }
}
