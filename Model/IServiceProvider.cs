namespace DependencyInjection.Model;

public interface IServiceProvider
{
    public IScope CreateScope();
}