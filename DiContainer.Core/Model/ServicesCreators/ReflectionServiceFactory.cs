using System.Reflection;
using DiContainer.Core.Helpers;

namespace DiContainer.Core.Model.ServicesCreators;

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

            return ReflectionHelper.Instantiate(ctor, dependencies);
        };
    }
}