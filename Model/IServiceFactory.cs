namespace DependencyInjection.Model;

internal interface IServiceFactory
{
    public TService Create<TService>(IScope scope);
}