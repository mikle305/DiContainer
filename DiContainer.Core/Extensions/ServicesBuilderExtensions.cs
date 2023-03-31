using System;
using DiContainer.Core.Model;

namespace DiContainer.Core.Extensions
{
    public static class ServicesBuilderExtensions
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


        private static IContainerBuilder RegisterTypeBased(
            this IContainerBuilder containerBuilder,
            Type serviceType,
            Type serviceImplementation,
            LifeTime lifeTime)
        {
            containerBuilder.Register(new TypeBasedServiceDescriptor(serviceType, lifeTime, serviceImplementation));

            return containerBuilder;
        }

        private static IContainerBuilder RegisterFactoryBased(
            this IContainerBuilder builder,
            Type serviceType,
            Func<IScope, object> factory,
            LifeTime lifeTime)
        {
            builder.Register(new FactoryBasedServiceDescriptor(serviceType, lifeTime, factory));

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
}