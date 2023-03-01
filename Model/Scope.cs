using System.Reflection;

namespace DependencyInjection.Model;

internal class Scope : IScope
{
    private readonly Dictionary<Type, ServiceDescriptor> _descriptors;

    public Scope(Dictionary<Type, ServiceDescriptor> descriptors)
    {
        _descriptors = descriptors;
    }

    public TService Resolve<TService>() 
        => (TService) CreateService(typeof(TService), this);
        
    private object CreateService(Type serviceType, IScope scope)
    {
        if (!_descriptors.TryGetValue(serviceType, out ServiceDescriptor descriptor))
            throw new InvalidOperationException($"Service {serviceType} is not registered");

        switch (descriptor)
        {
            case InstanceBasedServiceDescriptor instanceBased:
                return instanceBased.Instance;
            
            case FactoryBasedServiceDescriptor factoryBased:
                return factoryBased.Factory(scope);
            
            case TypeBasedServiceDescriptor typeBased:
                return CreateTypeBasedService(descriptor: typeBased, scope);
            
            default:
                throw new InvalidOperationException("Invalid descriptor type");
        }
    }

    private object CreateTypeBasedService(TypeBasedServiceDescriptor descriptor, IScope scope)
    {
        ConstructorInfo? constructor = descriptor
            .ImplementationType
            .GetConstructors(BindingFlags.Public | BindingFlags.Instance)
            .SingleOrDefault();
        
        if (constructor is null)
            throw new InvalidOperationException($"Service {descriptor.ImplementationType} must have only one constructor");

        ParameterInfo[] parameters = constructor.GetParameters();
        var dependencies = new object[parameters.Length];

        for (var i = 0; i < parameters.Length; i++)
            dependencies[i] = CreateService(parameters[i].ParameterType, scope);

        return constructor.Invoke(dependencies);
    }
}