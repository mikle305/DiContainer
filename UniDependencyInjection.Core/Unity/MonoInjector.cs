using System.Reflection;
using UniDependencyInjection.Core.Model;
using UnityEngine;

namespace DiContainer.UniDependencyInjection.Core.Unity
{
    public static class MonoInjector
    {
        /// <summary>
        /// Injects dependencies in all components members marked as InjectAttribute of game object.
        /// </summary>
        public static void InjectInMono(GameObject gameObject)
        {
            Component[] components = gameObject.GetComponents<Component>();
            InjectInComponents(components);
        }

        /// <summary>
        /// Injects dependencies in all components members marked as InjectAttribute of game object.
        /// </summary>
        public static void InjectInMono(Component component)
        {
            Component[] components = component.GetComponents<Component>();
            InjectInComponents(components);
        }

        /// <summary>
        /// Injects dependencies in all components members marked as InjectAttribute of game object.
        /// Components of children objects included.
        /// </summary>
        public static void InjectInMonoChildren(GameObject gameObject)
        {
            Component[] components = gameObject.GetComponentsInChildren<Component>();
            InjectInComponents(components);
        }
        
        /// <summary>
        /// Injects dependencies in all components members marked as InjectAttribute of game object.
        /// Components of children objects included.
        /// </summary>
        public static void InjectInMonoChildren(Component component)
        {
            Component[] components = component.GetComponentsInChildren<Component>();
            InjectInComponents(components);
        }

        private static void InjectInComponents(Component[] components)
        {
            IScope scope = ContainerAccess.Container.CreateScope();
            
            for (var i = 0; i < components.Length; i++)
            {
                InjectInComponent(scope, components[i]);
            }
        }

        private static void InjectInComponent(IScope scope, Component component)
        {
            foreach (MemberInfo member in component.GetType().GetMembers())
            {
                if (member.GetCustomAttribute<InjectAttribute>() == null)
                    continue;
                
                switch (member)
                {
                    case MethodInfo method:
                        InjectInMethod(scope, component, method);
                        continue;
                    case PropertyInfo property:
                        InjectInMethod(scope, component, property.SetMethod);
                        continue;
                }
            }
        }

        private static void InjectInMethod(IScope scope, Component component, MethodInfo method)
        {
            ParameterInfo[] args = method.GetParameters();
            var dependencies = new object[args.Length];

            for (var i = 0; i < args.Length; i++)
                dependencies[i] = scope.Resolve(args[i].ParameterType);

            method.Invoke(component, dependencies);
        }
    }
}