namespace DependencyInjection.Model;

internal class Scope : IScope
{
    private readonly IContainerProvider _containerProvider;
    

    public Scope(IContainerProvider containerProvider)
    {
        _containerProvider = containerProvider;
    }

    public TService Resolve<TService>()
    {
        ServiceDescriptor? descriptor = _containerProvider.GetDescriptor<TService>();

        if (descriptor == null)
            throw new InvalidOperationException($"{typeof(TService)} is not registered");
        
        if (descriptor.LifeTime == LifeTime.Transient)
            return _containerProvider.CreateInstance<TService>(this);
    }
}