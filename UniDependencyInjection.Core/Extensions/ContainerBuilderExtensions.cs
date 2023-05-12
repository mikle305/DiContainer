using System;
using UniDependencyInjection.Core.Model;
using UniDependencyInjection.Core.Model.Descriptors;

namespace UniDependencyInjection.Core.Extensions
{
    public static class ContainerBuilderExtensions
    {
        public static IContainerBuilder RegisterSingle<TService, TImplementation>(this IContainerBuilder builder)
            where TService : class
            where TImplementation : class, TService
        {
            return builder.RegisterTypeBased(
                serviceType: typeof(TService),
                serviceImplementation: typeof(TImplementation),
                lifeTime: LifeTime.Single);
        }

        public static IContainerBuilder RegisterScoped<TService, TImplementation>(this IContainerBuilder builder)
            where TService : class
            where TImplementation : class, TService
        {
            return builder.RegisterTypeBased(
                serviceType: typeof(TService),
                serviceImplementation: typeof(TImplementation),
                lifeTime: LifeTime.Scoped);
        }


        public static IContainerBuilder RegisterTransient<TService, TImplementation>(this IContainerBuilder builder)
            where TService : class
            where TImplementation : class, TService
        {
            return builder.RegisterTypeBased(
                serviceType: typeof(TService),
                serviceImplementation: typeof(TImplementation),
                lifeTime: LifeTime.Transient);
        }


        public static IContainerBuilder RegisterSingle<TService>(this IContainerBuilder builder)
            where TService : class
        {
            return builder.RegisterTypeBased(
                serviceType: typeof(TService),
                serviceImplementation: typeof(TService),
                lifeTime: LifeTime.Single);
        }

        public static IContainerBuilder RegisterScoped<TService>(this IContainerBuilder builder)
            where TService : class
        {
            return builder.RegisterTypeBased(
                serviceType: typeof(TService),
                serviceImplementation: typeof(TService),
                lifeTime: LifeTime.Scoped);
        }

        public static IContainerBuilder RegisterTransient<TService>(this IContainerBuilder builder)
            where TService : class
        {
            return builder.RegisterTypeBased(
                serviceType: typeof(TService),
                serviceImplementation: typeof(TService),
                lifeTime: LifeTime.Transient);
        }


        public static IContainerBuilder RegisterSingle<TService>(this IContainerBuilder builder,
            Func<IScope, TService> factory)
            where TService : class
        {
            return builder.RegisterFactoryBased(
                serviceType: typeof(TService),
                factory: factory,
                lifeTime: LifeTime.Single);
        }

        public static IContainerBuilder RegisterScoped<TService>(this IContainerBuilder builder,
            Func<IScope, TService> factory)
            where TService : class
        {
            return builder.RegisterFactoryBased(
                serviceType: typeof(TService),
                factory: factory,
                lifeTime: LifeTime.Scoped);
        }

        public static IContainerBuilder RegisterTransient<TService>(this IContainerBuilder builder,
            Func<IScope, TService> factory)
            where TService : class
        {
            return builder.RegisterFactoryBased(
                serviceType: typeof(TService),
                factory: factory,
                lifeTime: LifeTime.Transient);
        }


        public static IContainerBuilder RegisterSingle<TService>(this IContainerBuilder builder, TService instance)
            where TService : class
        {
            return builder.RegisterInstanceBased(
                serviceType: typeof(TService),
                instance: instance);
        }
    }
}