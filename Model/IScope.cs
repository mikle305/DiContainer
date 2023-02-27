namespace DependencyInjection.Model;

public interface IScope
{
    public TService Resolve<TService>();
}