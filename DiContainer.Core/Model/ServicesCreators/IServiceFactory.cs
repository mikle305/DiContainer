using System;

namespace DiContainer.Core.Model.ServicesCreators
{
    internal interface IServiceFactory
    {
        public object Create(IScope scope, Type serviceType);
    }
}
