using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using UniDependencyInjection.Core;
using UniDependencyInjection.Unity;
using UnityEngine;
using Object = UnityEngine.Object;

namespace UniDependencyInjection
{
    public class Container : IMonoResolver
    {
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

        public void Inject<T>(T instance) 
            where T : Object
        {
            switch (instance)
            {
                case GameObject gameObject:
                    InjectGameObject(gameObject);
                    break;
                case Component component:
                    InjectGameObject(component.gameObject);
                    break;
            }
        }

        private void InjectGameObject(GameObject gameObject)
        {
            Component[] components = gameObject.GetComponentsInChildren<Component>(includeInactive: true);
            IScope scope = CreateScope();
            
            foreach (Component component in components)
            {
                InjectionMembersInfo injectionMembers = TypeAnalyzer.FindInjectionMembers(component);
                InjectInMethods(scope, component, injectionMembers.Methods);
                InjectInProperties(scope, component, injectionMembers.Properties);
            }
        }

        private static void InjectInMethods(IScope scope, object obj, List<MethodInfo> injectionMethods)
        {
            if (injectionMethods == null)
                return;

            foreach (MethodInfo injectionMethod in injectionMethods)
            {
                ParameterInfo[] args = injectionMethod.GetParameters();
                var dependencies = new object[args.Length];

                for (var i = 0; i < args.Length; i++)
                    dependencies[i] = scope.Resolve(args[i].ParameterType);

                injectionMethod.Invoke(obj, dependencies);
            }
        }

        private static void InjectInProperties(IScope scope, object obj, List<PropertyInfo> injectionProperties)
        {
            if (injectionProperties == null)
                return;

            foreach (PropertyInfo injectionProperty in injectionProperties)
            {
                Type propertyType = injectionProperty.PropertyType;
                injectionProperty.SetValue(obj, scope.Resolve(propertyType));
            }
        }

        private static IServiceFactory CreateServiceFactory(Type serviceFactoryType, IDictionary<Type, ServiceDescriptor> descriptors)
        {
            ConstructorInfo ctor = TypeAnalyzer.FindSingleConstructor(serviceFactoryType);
            int argsCount = ctor.GetParameters().Length;
            if (argsCount != Constants.ServiceFactoryCtorArgs)
                ExceptionsHelper.ThrowCtorArgsCount(serviceFactoryType, Constants.ServiceFactoryCtorArgs);

            var parameters = new object[argsCount];
            parameters[0] = descriptors;

            return (IServiceFactory) ctor.Invoke(parameters);
        }
    }
}
