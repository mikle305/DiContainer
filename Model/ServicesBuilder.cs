namespace DependencyInjection.Model;

public class ServicesBuilder : IServicesBuilder
{
    private List<ServiceDescriptor> _services = new();

    
    public void Register(ServiceDescriptor serviceDescriptor)
    {
        throw new NotImplementedException();
    }

    public IServiceProvider Build()
    {
        throw new NotImplementedException();
    }
}