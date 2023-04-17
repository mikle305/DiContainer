using System;
using Cauldron.Interception;
using UniDependencyInjection.Core.Helpers;
using UnityEngine;

namespace DiContainer.UniDependencyInjection.Core.Unity
{
    [AttributeUsage(AttributeTargets.Method)]
    public class MonoFactoryAttribute : Attribute, IMethodInterceptorOnExit
    {
        private readonly bool _injectInChildren;
        

        public MonoFactoryAttribute()
        {
        }

        public MonoFactoryAttribute(bool injectInChildren)
        {
            _injectInChildren = injectInChildren;
        }
        
        public object OnExit(Type returnType, object returnValue)
        {
            switch (returnValue)
            {
                case GameObject objectValue when _injectInChildren:
                    MonoInjector.InjectInMonoChildren(objectValue);
                    break;
                case GameObject objectValue:
                    MonoInjector.InjectInMono(objectValue);
                    break;
                case Component componentValue when _injectInChildren:
                    MonoInjector.InjectInMonoChildren(componentValue);
                    break;
                case Component componentValue:
                    MonoInjector.InjectInMono(componentValue);
                    break;
                default:
                    ExceptionsHelper.ThrowMonoFactoryReturningType();
                    break;
            }

            return returnValue;
        }
    }
}