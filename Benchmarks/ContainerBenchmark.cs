using BenchmarkDotNet.Attributes;
using DependencyInjection.Extensions;
using DependencyInjection.Model;
using DependencyInjection.Model.Factory;

namespace Benchmarks;

[MemoryDiagnoser]
public class ContainerBenchmark
{
    private readonly IScope _reflectionBased, _lambdaBased;

    public ContainerBenchmark()
    {
        _reflectionBased = InitCustomContainer(
            new ContainerBuilder().WithCustomFactory<ReflectionServiceFactory>());

        _lambdaBased = InitCustomContainer(
            new ContainerBuilder().WithCustomFactory<LambdaServiceFactory>());
    }

    [Benchmark(Baseline = true)]
    public Controller New() 
        => new Controller(new Service(new Repository()));

    [Benchmark]
    public Controller Reflection()
        => _reflectionBased.Resolve<Controller>();

    [Benchmark]
    public Controller Lambda()
        => _lambdaBased.Resolve<Controller>();
    

    private IScope InitCustomContainer(IContainerBuilder containerBuilder)
        => containerBuilder
            .RegisterTransient<Repository>()
            .RegisterTransient<Controller>()
            .RegisterTransient<Service>()
            .Build()
            .CreateScope();
}

public class Controller
{
    private readonly Service _service;

    public Controller(Service service)
    {
        _service = service;
    }
}

public class Service
{
    private readonly Repository _repository;

    public Service(Repository repository)
    {
        _repository = repository;
    }
}

public class Repository
{
}
