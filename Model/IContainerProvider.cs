namespace DependencyInjection.Model;

internal interface IContainerProvider
{
    public object CreateInstance(IScope scope, Type serviceType);

    public ServiceDescriptor? GetDescriptor(Type serviceType);

    public IScope GetRootScope();
}