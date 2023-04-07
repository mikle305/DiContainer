using System;

namespace UniDependencyInjection.Core.Model.ServiceCreators
{
    internal interface IServiceFactory
    {
        public object CreateService(IScope scope, Type serviceType);
    }
}
