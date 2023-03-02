namespace DependencyInjection.Model;

internal interface IServiceFactory
{
    public object Create<TService>(IScope scope);
}