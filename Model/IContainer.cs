namespace DependencyInjection.Model;

public interface IContainer : IDisposable, IAsyncDisposable
{
    public IScope CreateScope();
}