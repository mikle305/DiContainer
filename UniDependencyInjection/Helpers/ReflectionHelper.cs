﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace UniDependencyInjection.Helpers
{
    public static class ReflectionHelper
    {
        public static ConstructorInfo FindSingleConstructor(Type type) 
            => type
                .GetConstructors(BindingFlags.Public | BindingFlags.Instance)
                .SingleOrDefault();
        
        public static IEnumerable<MethodInfo> FindMethodOverloads(Type type, string methodName)
            => type
                .GetMethods(BindingFlags.Public | BindingFlags.Instance)
                .Where(m => m.Name == methodName);
    }
}
