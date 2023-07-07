using System;

namespace UniDependencyInjection.Core
{
    internal class TypeBasedServiceDescriptor : ServiceDescriptor
    {
        public Type ImplementationType { get; }

        public TypeBasedServiceDescriptor(Type serviceType, LifeTime lifeTime, Type implementationType) 
            : base(serviceType, lifeTime)
        {
            ImplementationType = implementationType;
        }
    }
}