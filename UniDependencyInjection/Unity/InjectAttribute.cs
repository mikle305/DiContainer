using System;

namespace UniDependencyInjection.Unity
{
#if UNITY_2018_4_OR_NEWER
    [JetBrains.Annotations.MeansImplicitUse(
        JetBrains.Annotations.ImplicitUseKindFlags.Access |
        JetBrains.Annotations.ImplicitUseKindFlags.Assign |
        JetBrains.Annotations.ImplicitUseKindFlags.InstantiatedNoFixedConstructorSignature)]
#endif
    [AttributeUsage(validOn: 
        AttributeTargets.Method | 
        AttributeTargets.Property)]
    public class InjectAttribute : Attribute
    {
    }
}