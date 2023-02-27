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
        => CreateInstance(typeof(TService)) is TService ? (TService)CreateInstance(typeof(TService)) : default;
        
    private object CreateInstance(Type service)
    {
        if (!_descriptors.TryGetValue(service, out ServiceDescriptor descriptor))
            throw new InvalidOperationException($"Service {service} is not registered");

        ConstructorInfo? constructor = service.GetConstructors().SingleOrDefault();
        if (constructor is null)
            throw new InvalidOperationException($"Service {service} must have only one constructor");
        
        ParameterInfo[] parameters = constructor.GetParameters();
        var dependencies = new object[parameters.Length];

        for (var i = 0; i < parameters.Length; i++)
        {
            dependencies[i] = CreateInstance(parameters[i].ParameterType);
        }

        return constructor.Invoke(dependencies);
    }
}