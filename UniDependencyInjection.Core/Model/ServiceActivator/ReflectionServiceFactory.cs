using System;
using System.Collections.Generic;
using System.Reflection;
using UniDependencyInjection.Core.Model;
using UniDependencyInjection.Core.Model.Descriptors;

namespace DiContainer.UniDependencyInjection.Core.Model.ServiceActivator
{
    public class ReflectionServiceFactory : ServiceFactory
    {
        public ReflectionServiceFactory(IDictionary<Type, ServiceDescriptor> descriptorsMap) : base(descriptorsMap)
        {
        }

        protected override Func<IScope, object> CreateCtorInvoker(ConstructorInfo ctor, ParameterInfo[] args)
        {
            return s =>
            {
                var dependencies = new object[args.Length];

                for (var i = 0; i < args.Length; i++)
                    dependencies[i] = s.Resolve(args[i].ParameterType);

                return ctor.Invoke(dependencies);
            };
        }
    }
}
