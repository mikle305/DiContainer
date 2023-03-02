using System.Reflection;

namespace DependencyInjection.Model.Factory;

internal class LambdaServiceFactory : ServiceFactory
{
    public LambdaServiceFactory(IDictionary<Type, ServiceDescriptor> descriptorsMap) : base(descriptorsMap)
    {
    }

    protected override Func<IScope, object> CreateCtorInvoker(ConstructorInfo ctor, ParameterInfo[] args)
    {
        throw new NotImplementedException();
    }
}