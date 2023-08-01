using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UniDependencyInjection.Unity;
using UnityEngine;

namespace UniDependencyInjection
{
    public static class TypeAnalyzer
    {
        public static ConstructorInfo FindSingleConstructor(Type type) 
            => type
                .GetConstructors(BindingFlags.Public | BindingFlags.Instance)
                .SingleOrDefault();

        public static MethodInfo FindSingleMethod(Type type, string methodName)
            => type
                .GetMethods(BindingFlags.Public | BindingFlags.Instance)
                .Where(m => m.Name == methodName)
                .SingleOrDefault(m => !m.IsGenericMethod && m.Name == methodName);

        public static InjectionMembersInfo FindInjectionMembers(Component component)
        {
            Type type = component.GetType();
            List<MethodInfo> injectionMethods = null;
            List<PropertyInfo> injectionProperties = null;
            
            while (type != null && type != typeof(MonoBehaviour) && type != typeof(Component))
            {
                AddInjectionMethods(type);
                AddInjectionProperties(type);
                type = type.BaseType;
            }

            return new InjectionMembersInfo()
            {
                Methods = injectionMethods,
                Properties = injectionProperties,
            };
            



            void AddInjectionMethods(Type t)
            {
                MethodInfo[] methods = t.GetMethods(Constants.InjectionMembersFlags);
                foreach (MethodInfo method in methods)
                {
                    if (!method.IsDefined(typeof(InjectAttribute), false))
                        continue;

                    injectionMethods ??= new List<MethodInfo>();
                    if (injectionMethods.Exists(m => m.GetBaseDefinition() == method.GetBaseDefinition()))
                        continue;
                    
                    injectionMethods.Add(method);
                }
            }
            
            void AddInjectionProperties(Type t)
            {
                PropertyInfo[] properties = t.GetProperties(Constants.InjectionMembersFlags);
                foreach (PropertyInfo property in properties)
                {
                    if (!property.IsDefined(typeof(InjectAttribute), false))
                        continue;

                    injectionProperties ??= new List<PropertyInfo>();
                    if (injectionProperties.Exists(p => p.Name == property.Name))
                        continue;
                    
                    injectionProperties.Add(property);
                }
            }
        }
    }
}