using System;

namespace UniDependencyInjection.Core.Model
{
    public interface IContainer : IDisposable, IAsyncDisposable
    {
        public IScope CreateScope();
    }
}
