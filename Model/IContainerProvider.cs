namespace DependencyInjection.Model;

internal interface IContainerProvider
{
    public TService CreateInstance<TService>(IScope scope);

    public ServiceDescriptor? GetDescriptor<TService>();
}