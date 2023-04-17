using UniDependencyInjection.Core.Helpers;
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
            if (ContainerAccess.Container != null)
                ExceptionsHelper.ThrowMultipleContainersNotSupported();
            
            IContainerBuilder containerBuilder = new ContainerBuilder();
            ConfigureServices(containerBuilder);
            IContainer container = containerBuilder.Build();
            ContainerAccess.Container = container;
            return container;
        }
        
        protected virtual void ConfigureServices(IContainerBuilder containerBuilder)
        {
        }
    }
}