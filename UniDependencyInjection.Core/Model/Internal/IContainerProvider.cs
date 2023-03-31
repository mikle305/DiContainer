using System;
using UniDependencyInjection.Core.Model.Descriptors;

namespace UniDependencyInjection.Core.Model.Internal
{
    internal interface IContainerProvider
    {
        public object CreateInstance(IScope scope, Type serviceType);

        public ServiceDescriptor GetDescriptor(Type serviceType);

        public IScope GetRootScope();
    }
}
