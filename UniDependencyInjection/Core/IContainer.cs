using System;

namespace UniDependencyInjection.Core
{
    public interface IContainer : IDisposable, IAsyncDisposable
    {
        public IScope CreateScope();
    }
}
