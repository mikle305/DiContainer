using System.Reflection;

namespace UniDependencyInjection
{
    public static class Constants
    {
        public const int ServiceFactoryCtorArgs = 1;
        
        public const BindingFlags InjectionMembersFlags = 
            BindingFlags.Public | 
            BindingFlags.NonPublic | 
            BindingFlags.Instance | 
            BindingFlags.DeclaredOnly;
    }
}