namespace DependencyInjection.Model.Factory;

internal interface IServiceFactory
{
    public object Create(IScope scope, Type serviceType);
}