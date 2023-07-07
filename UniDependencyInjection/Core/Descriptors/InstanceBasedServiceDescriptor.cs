using System;

namespace UniDependencyInjection.Core
{
    internal class InstanceBasedServiceDescriptor : ServiceDescriptor
    {
        public object Instance { get; }

        public InstanceBasedServiceDescriptor(Type serviceType, object instance) : base(serviceType, LifeTime.Single)
        {
            Instance = instance;
        }
    }
}

