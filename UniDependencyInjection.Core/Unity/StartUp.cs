using UniDependencyInjection.Core.Model;

namespace DiContainer.UniDependencyInjection.Core.Unity
{
    public class StartUp
    {
        internal StartUp()
        {
        }

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