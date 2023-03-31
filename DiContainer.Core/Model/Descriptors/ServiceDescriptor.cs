using System;

namespace DiContainer.Core.Model
{
    public abstract class ServiceDescriptor
    {
        public Type ServiceType { get; protected set; }

        public LifeTime LifeTime { get; protected set; }
        
        protected ServiceDescriptor(Type serviceType, LifeTime lifeTime)
        {
            ServiceType = serviceType;
            LifeTime = lifeTime;
        }
    }
}

