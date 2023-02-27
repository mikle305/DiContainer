using System;

namespace DependencyInjection.Model;

public class ServicesBuilder : IServicesBuilder
{
    private ServiceProvider _services = new();


    public void Register(ServiceDescriptor serviceDescriptor)
    {
        throw new NotImplementedException();
    }

    public IServiceProvider Build() 
        => _services;
}