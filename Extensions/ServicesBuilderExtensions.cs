using DependencyInjection.Constants;
using DependencyInjection.Model;

namespace DependencyInjection.Extensions;

public static class ServicesBuilderExtensions
{
    public static IContainerBuilder RegisterSingle<TService, TImplementation>(this IContainerBuilder builder) where TService : class
        => builder.RegisterTypeBased(
            serviceType: typeof(TService),
            serviceImplementation: typeof(TImplementation), 
            lifeTime: LifeTime.Single);
    
    public static IContainerBuilder RegisterScoped<TService, TImplementation>(this IContainerBuilder builder) 
        => builder.RegisterTypeBased(
            serviceType: typeof(TService),
            serviceImplementation: typeof(TImplementation), 
            lifeTime: LifeTime.Scoped);

    public static IContainerBuilder RegisterTransient<TService, TImplementation>(this IContainerBuilder builder) 
        => builder.RegisterTypeBased(
            serviceType: typeof(TService),
            serviceImplementation: typeof(TImplementation), 
            lifeTime: LifeTime.Transient);
    
    
    public static IContainerBuilder RegisterSingle<TService>(this IContainerBuilder builder, Func<IScope, TService> factory)
        => builder.RegisterFactoryBased(
            serviceType: typeof(TService), 
            factory: s => 
                factory(s) ?? throw new InvalidOperationException(ExceptionMessages.ServiceFactoryReturnsNull), 
            lifeTime: LifeTime.Single);
    
    public static IContainerBuilder RegisterScoped<TService>(this IContainerBuilder builder, Func<IScope, TService> factory)
        => builder.RegisterFactoryBased(
            serviceType: typeof(TService), 
            factory: s => 
                factory(s) ?? throw new InvalidOperationException(ExceptionMessages.ServiceFactoryReturnsNull),
            lifeTime: LifeTime.Scoped);

    public static IContainerBuilder RegisterTransient<TService>(this IContainerBuilder builder, Func<IScope, TService> factory)
        => builder.RegisterFactoryBased(
            serviceType: typeof(TService), 
            factory: s => 
                factory(s) ?? throw new InvalidOperationException(ExceptionMessages.ServiceFactoryReturnsNull),
            lifeTime: LifeTime.Transient);

    
    public static IContainerBuilder RegisterSingle<TService>(this ContainerBuilder builder, TService instance) 
        => builder.RegisterInstanceBased(
            serviceType: typeof(TService), 
            instance ?? throw new ArgumentNullException(nameof(instance)));
    
    
    public static IContainerBuilder RegisterSingle<TService>(this IContainerBuilder builder) where TService : class
        => builder.RegisterTypeBased(
            serviceType: typeof(TService), 
            serviceImplementation: typeof(TService), 
            lifeTime: LifeTime.Single);
    
    public static IContainerBuilder RegisterScoped<TService>(this IContainerBuilder builder) where TService : class
        => builder.RegisterTypeBased(
            serviceType: typeof(TService), 
            serviceImplementation: typeof(TService), 
            lifeTime: LifeTime.Scoped);

    public static IContainerBuilder RegisterTransient<TService>(this IContainerBuilder builder) where TService : class
        => builder.RegisterTypeBased(
            serviceType: typeof(TService), 
            serviceImplementation: typeof(TService), 
            lifeTime: LifeTime.Transient);

    
    private static IContainerBuilder RegisterTypeBased(
        this IContainerBuilder containerBuilder, 
        Type serviceType,
        Type serviceImplementation,
        LifeTime lifeTime)
    {
        containerBuilder.Register(new TypeBasedServiceDescriptor
        {
            ServiceType = serviceType,
            ImplementationType = serviceImplementation,
            LifeTime = lifeTime,
        });

        return containerBuilder;
    }

    private static IContainerBuilder RegisterFactoryBased(
        this IContainerBuilder builder,
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

    private static IContainerBuilder RegisterInstanceBased(
        this IContainerBuilder builder,
        Type serviceType,
        object instance)
    {
        builder.Register(new InstanceBasedServiceDescriptor(serviceType, instance));

        return builder;
    }
}