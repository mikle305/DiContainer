using System;

namespace UniDependencyInjection.Core.Model.Descriptors
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

