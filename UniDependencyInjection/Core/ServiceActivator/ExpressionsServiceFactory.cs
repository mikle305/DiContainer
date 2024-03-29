﻿using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;

namespace UniDependencyInjection.Core
{
    public class ExpressionsServiceFactory : ServiceFactory
    {
        private static MethodInfo _resolveMethod;

    
        public ExpressionsServiceFactory(IDictionary<Type, ServiceDescriptor> descriptorsMap) 
            : base(descriptorsMap)
        {
            _resolveMethod = TypeAnalyzer.FindSingleMethod(typeof(IScope), nameof(IScope.Resolve));
        }

        protected override Func<IScope, object> CreateCtorInvoker(ConstructorInfo ctor, ParameterInfo[] args)
        {
            ParameterExpression scopeParameter = Expression.Parameter(typeof(IScope), "scope");

            var resolveDependencyExpressions = new Expression[args.Length];
            for (var i = 0; i < args.Length; i++)
            {
                ConstantExpression serviceParameter = Expression.Constant(args[i].ParameterType);

                // scope.Resolve(Type service1)
                resolveDependencyExpressions[i] = Expression.Convert(
                    Expression.Call(scopeParameter, _resolveMethod, serviceParameter), 
                    args[i].ParameterType);
            }

            // new Service(scope.Resolve(Type service1), ...)
            Expression ctorExpression = Expression.New(ctor, resolveDependencyExpressions);

            return Expression.Lambda<Func<IScope, object>>(ctorExpression, scopeParameter).Compile();
        }
    }   
}
