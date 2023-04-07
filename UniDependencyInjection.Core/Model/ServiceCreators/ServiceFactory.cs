using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Reflection;
using UniDependencyInjection.Core.Helpers;
using UniDependencyInjection.Core.Model.Descriptors;

namespace UniDependencyInjection.Core.Model.ServiceCreators
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
                ExceptionsHelper.ThrowServiceNotRegistered(serviceType.ToString());

            if (descriptor is InstanceBasedServiceDescriptor instanceBased)
                return _ => instanceBased.Instance;

            if (descriptor is FactoryBasedServiceDescriptor factoryBased)
                return factoryBased.Factory;

            var typeBased = (TypeBasedServiceDescriptor)descriptor;

            return CreateTypeBasedActivator(typeBased);
        }

        private Func<IScope, object> CreateTypeBasedActivator(TypeBasedServiceDescriptor descriptor)
        {
            Type implementationType = descriptor.ImplementationType;
            ConstructorInfo ctor = ReflectionHelper.FindSingleConstructor(implementationType);

            if (ctor is null)
                ExceptionsHelper.ThrowServiceSingleConstructor(implementationType.ToString());

            ParameterInfo[] args = ReflectionHelper.FindArguments(ctor);

            return CreateCtorInvoker(ctor, args);
        }

        protected abstract Func<IScope, object> CreateCtorInvoker(ConstructorInfo ctor, ParameterInfo[] args);
    }
}
