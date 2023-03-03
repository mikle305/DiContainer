using System.Collections.Immutable;
using System.Reflection;
using DependencyInjection.Helpers;
using DependencyInjection.Model.Factory;

namespace DependencyInjection.Model;

public class Container : IContainer
{
    private readonly IContainerProvider _containerProvider;


    internal Container(IEnumerable<ServiceDescriptor> services) : this(services, typeof(LambdaServiceFactory))
    {
    }

    internal Container(IEnumerable<ServiceDescriptor> services, Type serviceFactoryType)
    {
        IDictionary<Type, ServiceDescriptor> descriptors =
            services.ToImmutableDictionary(d => d.ServiceType);

        ConstructorInfo? ctor = ReflectionHelper.FindSingleConstructor(serviceFactoryType);
        ParameterInfo[] args = ReflectionHelper.FindArguments(ctor);
        
        var parameters = new object[args.Length];
        parameters[0] = descriptors;

        var serviceFactory = (ServiceFactory) ReflectionHelper.Instantiate(ctor, parameters);
        
        _containerProvider = new ContainerProvider(descriptors, serviceFactory);
    }

    public IScope CreateScope()
        => new Scope(_containerProvider);

    public void Dispose() 
        => _containerProvider.GetRootScope().Dispose();

    public ValueTask DisposeAsync() 
        => _containerProvider.GetRootScope().DisposeAsync();
}