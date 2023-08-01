using System;

namespace UniDependencyInjection.Core
{
    public interface IScope : IDisposable, IAsyncDisposable
    {
        public object Resolve(Type serviceType);

        public T Resolve<T>();
    }
}
