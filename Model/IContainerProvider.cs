namespace DependencyInjection.Model;

internal interface IContainerProvider
{
    public object CreateInstance<TService>(IScope scope);

    public ServiceDescriptor? GetDescriptor<TService>();

    public IScope GetRootScope();
}