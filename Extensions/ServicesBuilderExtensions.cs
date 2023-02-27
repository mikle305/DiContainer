using DependencyInjection.Model;

namespace DependencyInjection.Extensions;

public static class ServicesBuilderExtensions
{
    public static IServicesBuilder RegisterSingle<TService, TImplementation>(this IServicesBuilder builder) 
        => builder.RegisterType(typeof(TService), typeof(TImplementation), LifeTime.Single);
    
    public static IServicesBuilder RegisterScoped<TService, TImplementation>(this IServicesBuilder builder) 
        => builder.RegisterType(typeof(TService), typeof(TImplementation), LifeTime.Scoped);

    public static IServicesBuilder RegisterTransient<TService, TImplementation>(this IServicesBuilder builder) 
        => builder.RegisterType(typeof(TService), typeof(TImplementation), LifeTime.Transient);
    
    
    public static IServicesBuilder RegisterSingle<TService>(this IServicesBuilder builder, Func<IScope, TService> factory)
        => builder.RegisterFactory(typeof(TService), s => factory(s), LifeTime.Single);
    
    public static IServicesBuilder RegisterScoped<TService>(this IServicesBuilder builder, Func<IScope, TService> factory)
        => builder.RegisterFactory(typeof(TService), s => factory(s), LifeTime.Scoped);

    public static IServicesBuilder RegisterTransient<TService>(this IServicesBuilder builder, Func<IScope, TService> factory) 
        => builder.RegisterFactory(typeof(TService), s => factory(s), LifeTime.Transient);

    
    public static IServicesBuilder RegisterSingle<TService>(this ServicesBuilder builder, TService instance) 
        => builder.RegisterInstance(typeof(TService), instance);

    
    private static IServicesBuilder RegisterType(
        this IServicesBuilder servicesBuilder, 
        Type serviceType,
        Type serviceImplementation,
        LifeTime lifeTime)
    {
        servicesBuilder.Register(new TypeBasedServiceDescriptor
        {
            ServiceType = serviceType,
            ImplementationType = serviceImplementation,
            LifeTime = lifeTime,
        });

        return servicesBuilder;
    }

    private static IServicesBuilder RegisterFactory(
        this IServicesBuilder builder,
        Type serviceType,
        Func<IScope, object> factory,
        LifeTime lifeTime)
    {
        builder.Register(new FactoryBasedServiceDescriptor
        {
            ServiceType = serviceType,
            Factory = factory,
            LifeTime = lifeTime,
        });

        return builder;
    }

    private static IServicesBuilder RegisterInstance(
        this IServicesBuilder builder,
        Type serviceType,
        object instance)
    {
        builder.Register(new InstanceBasedServiceDescriptor(serviceType, instance));

        return builder;
    }
}