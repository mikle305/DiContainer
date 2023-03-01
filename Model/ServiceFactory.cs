using System.Collections.Concurrent;
using System.Reflection;
using DependencyInjection.Helpers;

namespace DependencyInjection.Model;

internal class ServiceFactory : IServiceFactory
{
    private readonly IDictionary<Type, ServiceDescriptor> _descriptorsMap;
    private readonly ConcurrentDictionary<Type, Func<IScope, object>> _activatorsMap;


    public ServiceFactory(IDictionary<Type, ServiceDescriptor> descriptorsMap)
    {
        _descriptorsMap = descriptorsMap;
        _activatorsMap = new ConcurrentDictionary<Type, Func<IScope, object>>();
    }

    public TService Create<TService>(IScope scope)
    {
        Type serviceType = typeof(TService);
        
        object service = _activatorsMap
            .GetOrAdd(key: serviceType, valueFactory: CreateActivator)
            .Invoke(scope);
        
        return (TService) service;
    }

    private Func<IScope, object> CreateActivator(Type serviceType)
    {
        if (!_descriptorsMap.TryGetValue(serviceType, out ServiceDescriptor? descriptor))
            throw new InvalidOperationException($"Service {serviceType} is not registered");

        if (descriptor is InstanceBasedServiceDescriptor instanceBased)
            return _ => instanceBased.Instance;

        if (descriptor is FactoryBasedServiceDescriptor factoryBased)
            return factoryBased.Factory;

        return CreateTypeBasedActivator((TypeBasedServiceDescriptor) descriptor);
    }

    private Func<IScope, object> CreateTypeBasedActivator(TypeBasedServiceDescriptor descriptor)
    {
        ConstructorInfo? ctor = descriptor
            .ImplementationType
            .GetConstructors(BindingFlags.Public | BindingFlags.Instance)
            .SingleOrDefault();

        if (ctor == null)
            ExceptionsHelper.ThrowServiceSingleConstructor(descriptor.ImplementationType.ToString());

        ParameterInfo[] args = ctor.GetParameters();

        return _ =>
        {
            var dependencies = new object[args.Length];

            for (var i = 0; i < args.Length; i++)
                dependencies[i] = CreateActivator(args[i].ParameterType);

            return ctor.Invoke(dependencies);
        };
    }
}