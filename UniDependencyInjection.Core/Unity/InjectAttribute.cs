using System;

namespace DiContainer.UniDependencyInjection.Core.Unity
{
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Property)]
    public class InjectAttribute : Attribute
    {
    }
}