using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using UniDependencyInjection.Helpers;

namespace UniDependencyInjection.Core
{
    public class Container : IContainer
    {
        private const int _factoryConstructorTargetArgs = 1;
        private IContainerProvider _containerProvider;


        internal Container()
        {
        }

        internal void Initialize(IEnumerable<ServiceDescriptor> services, Type serviceFactoryType)
        {
            IDictionary<Type, ServiceDescriptor> descriptors =
                services.ToDictionary(d => d.ServiceType);

            IServiceFactory serviceFactory = CreateServiceFactory(serviceFactoryType, descriptors);
            _containerProvider = new ContainerProvider(descriptors, serviceFactory);
        }

        public IScope CreateScope()
            => new Scope(_containerProvider);

        public void Dispose() 
            => _containerProvider.GetRootScope().Dispose();

        public ValueTask DisposeAsync() 
            => _containerProvider.GetRootScope().DisposeAsync();

        private static IServiceFactory CreateServiceFactory(Type serviceFactoryType, IDictionary<Type, ServiceDescriptor> descriptors)
        {
            ConstructorInfo ctor = ReflectionHelper.FindSingleConstructor(serviceFactoryType);
            int argsCount = ctor.GetParameters().Length;
            if (argsCount != _factoryConstructorTargetArgs)
                ExceptionsHelper.ThrowFunctionArgumentsCount(_factoryConstructorTargetArgs);

            var parameters = new object[argsCount];
            parameters[0] = descriptors;

            return (IServiceFactory) ctor.Invoke(parameters);
        }
    }
}
