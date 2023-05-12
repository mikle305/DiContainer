using System;
using System.Collections.Concurrent;
using System.Threading.Tasks;
using UniDependencyInjection.Core.Helpers;
using UniDependencyInjection.Core.Model.Descriptors;

namespace UniDependencyInjection.Core.Model.Internal
{
    internal class Scope : IScope
    {
        private readonly IContainerProvider _containerProvider;
        private readonly ConcurrentDictionary<Type, object> _scopedInstances = new();
        private readonly ConcurrentStack<object> _disposables = new();
    
    
        public Scope(IContainerProvider containerProvider)
        {
            _containerProvider = containerProvider;
        }
    
        public TService Resolve<TService>()
            => (TService) Resolve(typeof(TService));
    
        public object Resolve(Type serviceType)
        {
            ServiceDescriptor descriptor = _containerProvider.GetDescriptor(serviceType);
            IScope rootScope = _containerProvider.GetRootScope();
    
            if (descriptor is null)
                ExceptionsHelper.ThrowServiceNotRegistered(serviceType.ToString());
            
            if (descriptor.LifeTime == LifeTime.Transient)
                return CreateInstance(serviceType);
    
            if (descriptor.LifeTime == LifeTime.Scoped || this == rootScope)
                return _scopedInstances.GetOrAdd(serviceType, CreateInstance(serviceType));
    
            return rootScope.Resolve(serviceType);
        }
    
        private object CreateInstance(Type serviceType)
        {
            object instance = _containerProvider.CreateInstance(this, serviceType);
            if (instance is IDisposable or IAsyncDisposable)
                _disposables.Push(instance);
            
            return instance;
        }
    
        public void Dispose()
        {
            foreach (object disposable in _disposables)
            {
                switch (disposable)
                {
                    case IContainer:
                        continue;
                    case IDisposable d:
                        d.Dispose();
                        break;
                    case IAsyncDisposable:
                        ExceptionsHelper.ThrowAsyncDisposeInInvalidContext();
                        break;
                }
            }
        }
    
        public async ValueTask DisposeAsync()
        {
            foreach (object disposable in _disposables)
            {
                switch (disposable)
                {
                    case IContainer:
                        continue;
                    case IAsyncDisposable a:
                        await a.DisposeAsync();
                        break;
                    case IDisposable d:
                        d.Dispose();
                        break;
                }
            }
        }
    }    
}
