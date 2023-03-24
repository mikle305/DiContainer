using Autofac;
using BenchmarkDotNet.Attributes;
using DiContainer.Core.Extensions;
using DiContainer.Core.Model;
using DiContainer.Core.Model.ServicesCreators;
using Microsoft.Extensions.DependencyInjection;
using ContainerBuilder = DiContainer.Core.Model.ContainerBuilder;

namespace DiContainer.Benchmarks;

[MemoryDiagnoser]
public class ContainerBenchmark
{
    private readonly IScope _reflectionBased, _lambdaBased;
    private readonly ILifetimeScope _autofac;
    private readonly IServiceScope _msDi;


    public ContainerBenchmark()
    {
        _reflectionBased = InitCustomContainer(
            new ContainerBuilder().WithCustomServiceCreator<ReflectionServiceFactory>());

        _lambdaBased = InitCustomContainer(
            new ContainerBuilder().WithCustomServiceCreator<ExpressionsServiceFactory>());

        _autofac = InitAutofac();

        _msDi = InitMsDi();
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

    [Benchmark]
    public Controller Autofac()
        => _autofac.Resolve<Controller>();

    [Benchmark]
    public Controller MsDi()
        => _msDi.ServiceProvider.GetService<Controller>()!;


    private IScope InitCustomContainer(IContainerBuilder containerBuilder)
        => containerBuilder
            .RegisterTransient<Repository>()
            .RegisterTransient<Controller>()
            .RegisterTransient<Service>()
            .Build()
            .CreateScope();

    private ILifetimeScope InitAutofac()
    {
        var containerBuilder = new Autofac.ContainerBuilder();
        containerBuilder.RegisterType<Repository>().AsSelf().InstancePerDependency();
        containerBuilder.RegisterType<Controller>().AsSelf().InstancePerDependency();
        containerBuilder.RegisterType<Service>().AsSelf().InstancePerDependency();
        return containerBuilder.Build().BeginLifetimeScope();
    }

    private IServiceScope InitMsDi()
    {
        var collection = new ServiceCollection();
        collection.AddTransient<Repository>();
        collection.AddTransient<Controller>();
        collection.AddTransient<Service>();
        return collection.BuildServiceProvider().CreateScope();
    }
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
