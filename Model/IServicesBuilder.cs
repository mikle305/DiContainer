namespace DependencyInjection.Model;

public interface IServicesBuilder
{
    public void Register(ServiceDescriptor serviceDescriptor);
    
    public IServiceProvider Build();
}