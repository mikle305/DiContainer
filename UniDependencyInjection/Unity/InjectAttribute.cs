using System;

namespace UniDependencyInjection.Unity
{
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Property)]
    public class InjectAttribute : Attribute
    {
    }
}