using System;

namespace DependencyInjection.Model;

public class ServicesBuilder : IServicesBuilder
{
    private ServiceProvider _services = new();
    
    
    public void RegisterSingle<TService, TImplementation>()
    {
        throw new NotImplementedException();
    }

    public void RegisterScoped<TService, TImplementation>()
    {
        throw new NotImplementedException();
    }

    public void RegisterTransient<TService, TImplementation>()
    {
        throw new NotImplementedException();
    }

    public IServiceProvider Build() 
        => _services;
}