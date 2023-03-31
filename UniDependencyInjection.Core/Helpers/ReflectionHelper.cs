using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace UniDependencyInjection.Core.Helpers
{
    public static class ReflectionHelper
    {
        public static ConstructorInfo FindSingleConstructor(Type type) 
            => type
                .GetConstructors(BindingFlags.Public | BindingFlags.Instance)
                .SingleOrDefault();

        public static ParameterInfo[] FindArguments(ConstructorInfo constructor) 
            => constructor.GetParameters();

        public static IEnumerable<MethodInfo> FindMethodOverloads(Type type, string methodName)
            => type
                .GetMethods(BindingFlags.Public | BindingFlags.Instance)
                .Where(m => m.Name == methodName);

        public static object Instantiate(ConstructorInfo constructor, object[] parameters)
            => constructor.Invoke(parameters);
    }
}

