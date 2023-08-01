using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Reflection;

namespace UniDependencyInjection.Core
{
    public abstract class ServiceFactory : IServiceFactory
    {
        private readonly IDictionary<Type, ServiceDescriptor> _descriptorsMap;
        private readonly ConcurrentDictionary<Type, Func<IScope, object>> _activatorsMap;


        protected ServiceFactory(IDictionary<Type, ServiceDescriptor> descriptorsMap)
        {
            _descriptorsMap = descriptorsMap;
            _activatorsMap = new ConcurrentDictionary<Type, Func<IScope, object>>();
        }

        public object CreateService(IScope scope, Type serviceType)
        {
            object service = _activatorsMap
                .GetOrAdd(key: serviceType, valueFactory: CreateActivator)
                .Invoke(scope);

            return service;
        }

        private Func<IScope, object> CreateActivator(Type serviceType)
        {
            if (!_descriptorsMap.TryGetValue(serviceType, out ServiceDescriptor descriptor))
                ExceptionsHelper.ThrowServiceNotRegistered(serviceType);

            switch (descriptor)
            {
                case InstanceBasedServiceDescriptor instanceBased:
                    return _ => instanceBased.Instance;
                case FactoryBasedServiceDescriptor factoryBased:
                    return factoryBased.Factory;
                default:
                    return CreateTypeBasedActivator(descriptor);
            }
        }

        private Func<IScope, object> CreateTypeBasedActivator(ServiceDescriptor descriptor)
        {
            Type implementationType = ((TypeBasedServiceDescriptor)descriptor).ImplementationType;
            ConstructorInfo ctor = TypeAnalyzer.FindSingleConstructor(implementationType);

            if (ctor is null)
                ExceptionsHelper.ThrowServiceSingleConstructor(implementationType.ToString());

            ParameterInfo[] args = ctor!.GetParameters();

            return CreateCtorInvoker(ctor, args);
        }

        protected abstract Func<IScope, object> CreateCtorInvoker(ConstructorInfo ctor, ParameterInfo[] args);
    }
}
