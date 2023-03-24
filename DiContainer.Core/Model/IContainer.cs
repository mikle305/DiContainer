namespace DiContainer.Core.Model;

public interface IContainer : IDisposable, IAsyncDisposable
{
    public IScope CreateScope();
}