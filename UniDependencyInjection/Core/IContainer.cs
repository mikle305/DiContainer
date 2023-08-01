namespace UniDependencyInjection.Core
{
    public interface IContainer
    {
        public IScope CreateScope();
    }
}