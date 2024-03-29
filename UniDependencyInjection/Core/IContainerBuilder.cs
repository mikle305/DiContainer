﻿using System;

namespace UniDependencyInjection.Core
{
    public interface IContainerBuilder
    {
        public IContainerBuilder RegisterTypeBased(
            Type serviceType,
            Type serviceImplementation,
            LifeTime lifeTime);

        public IContainerBuilder RegisterFactoryBased(
            Type serviceType, 
            Func<IScope, object> factory, 
            LifeTime lifeTime);

        public IContainerBuilder RegisterInstanceBased(
            Type serviceType, 
            object instance);

        public IContainerBuilder WithCustomServiceActivatorFactory<TServiceFactory>() 
            where TServiceFactory : ServiceFactory;

        public IContainer Build();
    }
}

