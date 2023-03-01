namespace DependencyInjection.Model;

public interface IContainer
{
    public IScope CreateScope();
}