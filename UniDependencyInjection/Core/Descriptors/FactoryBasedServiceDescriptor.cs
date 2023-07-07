using System;

namespace UniDependencyInjection.Core
{
    internal class FactoryBasedServiceDescriptor : ServiceDescriptor
    {
        public Func<IScope, object> Factory { get; }

        public FactoryBasedServiceDescriptor(Type serviceType, LifeTime lifeTime, Func<IScope, object> factory)
            : base(serviceType, lifeTime)
        {
            Factory = factory;
        }
    }
}