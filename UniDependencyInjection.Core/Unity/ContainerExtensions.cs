using UniDependencyInjection.Core.Helpers;
using UniDependencyInjection.Core.Model;
using UnityEngine;

namespace DiContainer.UniDependencyInjection.Core.Unity
{
    public static class ContainerExtensions
    {
        public static T Instantiate<T>(
            this IContainer container, 
            T prefab,
            bool injectInChildren = false) 
            where T : Object
        {
            T obj = Object.Instantiate(prefab);
            TryInjectInMono(container, obj, injectInChildren);
            return obj;
        }
        
        public static T Instantiate<T>(
            this IContainer container, 
            T prefab, 
            Transform parent, 
            bool injectInChildren = false) 
            where T : Object
        {
            T obj = Object.Instantiate(prefab, parent);
            TryInjectInMono(container, obj, injectInChildren);
            return obj;
        }
        
        public static T Instantiate<T>(
            this IContainer container, 
            T prefab, 
            Vector3 position, 
            bool injectInChildren = false) 
            where T : Object
        {
            T obj = Object.Instantiate(prefab, position, Quaternion.identity);
            TryInjectInMono(container, obj, injectInChildren);
            return obj;
        }
        
        public static T Instantiate<T>(
            this IContainer container, 
            T prefab, 
            Vector3 position, 
            Transform parent, 
            bool injectInChildren = false) 
            where T : Object
        {
            T obj = Object.Instantiate(prefab, position, Quaternion.identity, parent);
            TryInjectInMono(container, obj, injectInChildren);
            return obj;
        }

        private static void TryInjectInMono(IContainer container, Object value, bool injectInChildren)
        {
            switch (value)
            {
                case GameObject objectValue when injectInChildren:
                    MonoInjector.InjectChildren(container, objectValue);
                    break;
                case GameObject objectValue:
                    MonoInjector.Inject(container, objectValue);
                    break;
                case Component componentValue when injectInChildren:
                    MonoInjector.InjectChildren(container, componentValue);
                    break;
                case Component componentValue:
                    MonoInjector.Inject(container, componentValue);
                    break;
                default:
                    ExceptionsHelper.ThrowUnhandledMonoInjectionType();
                    break;
            }
        }
    }
}