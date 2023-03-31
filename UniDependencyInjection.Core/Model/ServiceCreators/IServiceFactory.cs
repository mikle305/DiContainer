using System;

namespace UniDependencyInjection.Core.Model.ServiceCreators
{
    internal interface IServiceFactory
    {
        public object Create(IScope scope, Type serviceType);
    }
}
