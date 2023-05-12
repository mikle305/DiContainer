using System;
using UniDependencyInjection.Core.Model;

namespace DiContainer.UniDependencyInjection.Core.Model.ServiceActivator
{
    internal interface IServiceFactory
    {
        public object CreateService(IScope scope, Type serviceType);
    }
}
