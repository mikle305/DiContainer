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
        public static void Inject(IContainer container, GameObject gameObject)
        {
            Component[] components = gameObject.GetComponents<Component>();
            InjectInComponents(container, components);
        }

        /// <summary>
        /// Injects dependencies in all components members marked as InjectAttribute of game object.
        /// </summary>
        public static void Inject(IContainer container, Component component)
        {
            Component[] components = component.GetComponents<Component>();
            InjectInComponents(container, components);
        }

        /// <summary>
        /// Injects dependencies in all components members marked as InjectAttribute of game object.
        /// Components of children objects included.
        /// </summary>
        public static void InjectChildren(IContainer container, GameObject gameObject)
        {
            Component[] components = gameObject.GetComponentsInChildren<Component>();
            InjectInComponents(container, components);
        }
        
        /// <summary>
        /// Injects dependencies in all components members marked as InjectAttribute of game object.
        /// Components of children objects included.
        /// </summary>
        public static void InjectChildren(IContainer container, Component component)
        {
            Component[] components = component.GetComponentsInChildren<Component>();
            InjectInComponents(container, components);
        }

        private static void InjectInComponents(IContainer container, Component[] components)
        {
            IScope scope = container.CreateScope();

            foreach (Component component in components)
                InjectInComponent(scope, component);
        }

        private static void InjectInComponent(IScope scope, Component component)
        {
            FindInjectionMethods(scope, component);
            FindInjectionProperties(scope, component);
        }

        private static void FindInjectionMethods(IScope scope, object obj)
        {
            foreach (MethodInfo methodInfo in obj.GetType().GetMethods())
            {
                if (methodInfo.GetCustomAttribute<InjectAttribute>() != null)
                    InjectInMethod(scope, obj, methodInfo);
            }
        }
        
        private static void FindInjectionProperties(IScope scope, object obj)
        {
            foreach (PropertyInfo propertyInfo in obj.GetType().GetProperties())
            {
                MethodInfo setter = propertyInfo.GetSetMethod();
                if (setter != null)
                    InjectInMethod(scope, obj, setter);
            }
        }

        private static void InjectInMethod(IScope scope, object obj, MethodBase method)
        {
            ParameterInfo[] args = method.GetParameters();
            var dependencies = new object[args.Length];

            for (var i = 0; i < args.Length; i++)
                dependencies[i] = scope.Resolve(args[i].ParameterType);

            method.Invoke(obj, dependencies);
        }
    }
}