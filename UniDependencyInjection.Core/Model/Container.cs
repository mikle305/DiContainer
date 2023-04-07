using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Threading.Tasks;
using UniDependencyInjection.Core.Helpers;
using UniDependencyInjection.Core.Model.Descriptors;
using UniDependencyInjection.Core.Model.Internal;
using UniDependencyInjection.Core.Model.ServiceCreators;

namespace UniDependencyInjection.Core.Model
{
    public class Container : IContainer
    {
        private const int _factoryConstructorTargetArgs = 1;
        private readonly IContainerProvider _containerProvider;


        internal Container(IEnumerable<ServiceDescriptor> services) : this(services, typeof(ExpressionsServiceFactory))
        {
        }

        internal Container(IEnumerable<ServiceDescriptor> services, Type serviceFactoryType)
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
            int argsCount = ReflectionHelper.FindArguments(ctor).Length;
            if (argsCount != _factoryConstructorTargetArgs)
                ExceptionsHelper.ThrowFunctionArgumentsCount(_factoryConstructorTargetArgs);

            var parameters = new object[argsCount];
            parameters[0] = descriptors;

            return (IServiceFactory) ReflectionHelper.Instantiate(ctor, parameters);
        }
    }
}
