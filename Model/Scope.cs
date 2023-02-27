using System;

namespace DependencyInjection.Model;

public class Scope : IScope
{
    public TService Resolve<TService>()
    {
        throw new NotImplementedException();
    }
}