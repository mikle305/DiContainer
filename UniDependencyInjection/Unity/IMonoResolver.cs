using UniDependencyInjection.Core;

namespace UniDependencyInjection.Unity
{
    public interface IMonoResolver : IContainer
    {
        public void Inject<T>(T instance) where T : UnityEngine.Object;
    }
}