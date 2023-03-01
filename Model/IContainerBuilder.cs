namespace DependencyInjection.Model;

public interface IContainerBuilder
{
    public void Register(ServiceDescriptor serviceDescriptor);
    
    public IContainer Build();
}