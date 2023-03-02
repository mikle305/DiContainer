using System.Collections.Concurrent;
using DependencyInjection.Helpers;

namespace DependencyInjection.Model;

internal class Scope : IScope
{
    private readonly IContainerProvider _containerProvider;
    private readonly ConcurrentDictionary<Type, object> _scopedInstances = new();


    public Scope(IContainerProvider containerProvider)
    {
        _containerProvider = containerProvider;
    }

    public TService Resolve<TService>()
    {
        ServiceDescriptor? descriptor = _containerProvider.GetDescriptor<TService>();
        Type serviceType = typeof(TService);
        IScope rootScope = _containerProvider.GetRootScope();

        if (descriptor == null)
            ExceptionsHelper.ThrowServiceNotRegistered(serviceType.ToString());
        
        if (descriptor.LifeTime == LifeTime.Transient)
            return (TService) _containerProvider.CreateInstance<TService>(this);

        if (descriptor.LifeTime == LifeTime.Scoped || rootScope == this)
            return (TService) _scopedInstances.GetOrAdd(serviceType, _containerProvider.CreateInstance<TService>(this));

        return rootScope.Resolve<TService>();
    }
}