using System;
using System.Reflection;
using MethodDecorator.Fody.Interfaces;
using UniDependencyInjection.Core.Model;

namespace DiContainer.UniDependencyInjection.Core.Unity
{
    [AttributeUsage(AttributeTargets.Method)]
    public class InjectAttribute : Attribute, IMethodDecorator
    {
        public void Init(object instance, MethodBase method, object[] args)
        {
            IScope scope = ContainerAccess.Container.CreateScope();
            ParameterInfo[] parameters = method.GetParameters();
            var dependencies = new object[args.Length];
            
            for (var i = 0; i < args.Length; i++)
                dependencies[i] = scope.Resolve(parameters[i].ParameterType);

            method.Invoke(instance, dependencies);
        }

        public void OnEntry()
        {
        }

        public void OnExit()
        {
        }

        public void OnException(Exception exception)
        {
        }
    }
}