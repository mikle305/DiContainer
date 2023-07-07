using UniDependencyInjection.Core;

namespace UniDependencyInjection.Unity
{
    public class StartUp
    {
        public IContainer Build()
        {
            IContainerBuilder containerBuilder = new ContainerBuilder();
            ConfigureServices(containerBuilder);
            IContainer container = containerBuilder.Build();
            return container;
        }
        
        protected virtual void ConfigureServices(IContainerBuilder containerBuilder)
        {
        }
    }
}