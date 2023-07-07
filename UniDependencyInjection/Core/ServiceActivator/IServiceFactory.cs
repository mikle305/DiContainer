using System;

namespace UniDependencyInjection.Core
{
    internal interface IServiceFactory
    {
        public object CreateService(IScope scope, Type serviceType);
    }
}
