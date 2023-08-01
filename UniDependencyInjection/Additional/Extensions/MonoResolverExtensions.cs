using UniDependencyInjection.Unity;
using UnityEngine;

namespace UniDependencyInjection
{
    public static class MonoResolverExtensions
    {
        public static T Instantiate<T>(
            this IMonoResolver resolver, 
            T prefab) 
            where T : UnityEngine.Object
        {
            T instance = UnityEngine.Object.Instantiate(prefab);
            resolver.Inject(instance);
            return instance;
        }

        public static T Instantiate<T>(
            this IMonoResolver resolver, 
            T prefab, 
            Transform parent,
            bool worldPositionStays = false)
            where T : UnityEngine.Object
        {
            T instance = UnityEngine.Object.Instantiate(prefab, parent, worldPositionStays);
            resolver.Inject(instance);
            return instance;
        }

        public static T Instantiate<T>(
            this IMonoResolver resolver,
            T prefab,
            Vector3 position,
            Quaternion rotation)
            where T : UnityEngine.Object
        {
            T instance = UnityEngine.Object.Instantiate(prefab, position, rotation);
            resolver.Inject(instance);
            return instance;
        }

        public static T Instantiate<T>(
            this IMonoResolver resolver,
            T prefab,
            Vector3 position,
            Quaternion rotation,
            Transform parent)
            where T : UnityEngine.Object
        {
            T instance = UnityEngine.Object.Instantiate(prefab, position, rotation, parent);
            resolver.Inject(instance);
            return instance;
        }
    }
}