namespace DependencyInjection.Model;

public interface IServicesBuilder
{
    public void RegisterSingle<TInterface, TImplementation>();
    
    public void RegisterScoped<TService, TImplementation>();
    
    public void RegisterTransient<TService, TImplementation>();
    
    public IServiceProvider Build();
}