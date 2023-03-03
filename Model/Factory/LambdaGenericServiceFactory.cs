using System.Linq.Expressions;
using System.Reflection;
using DependencyInjection.Helpers;

namespace DependencyInjection.Model.Factory;

public class LambdaGenericServiceFactory : ServiceFactory
{
    private static MethodInfo? _resolveMethod;

    public LambdaGenericServiceFactory(IDictionary<Type, ServiceDescriptor> descriptorsMap) : base(descriptorsMap)
    {
        _resolveMethod = ReflectionHelper
            .FindMethodOverloads(typeof(IScope), nameof(IScope.Resolve))
            .SingleOrDefault(m => m.IsGenericMethod);
    }

    protected override Func<IScope, object> CreateCtorInvoker(ConstructorInfo ctor, ParameterInfo[] args)
    {
        ParameterExpression scopeParameter = Expression.Parameter(typeof(IScope), "scope");

        var resolveDependencyExpressions = new Expression[args.Length];
        for (var i = 0; i < args.Length; i++)
        {
            // scope.Resolve<IService>()
            resolveDependencyExpressions[i] =
                Expression.Call(scopeParameter, _resolveMethod.MakeGenericMethod(args[i].ParameterType));
        }

        // new Service(scope.Resolve<IService1>(), ...)
        Expression ctorExpression = Expression.New(ctor, resolveDependencyExpressions);

        return Expression.Lambda<Func<IScope, object>>(ctorExpression, scopeParameter).Compile();
    }
}