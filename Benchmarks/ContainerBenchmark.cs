using Autofac;
using BenchmarkDotNet.Attributes;
using DependencyInjection.Extensions;
using DependencyInjection.Model;
using DependencyInjection.Model.Factory;
using Microsoft.Extensions.DependencyInjection;
using ContainerBuilder = DependencyInjection.Model.ContainerBuilder;

namespace Benchmarks;

[MemoryDiagnoser]
public class ContainerBenchmark
{
    private readonly IScope _reflectionBased, _lambdaBased;
    private readonly ILifetimeScope _autofac;
    private readonly IServiceScope _msDi;


    public ContainerBenchmark()
    {
        _reflectionBased = InitCustomContainer(
            new ContainerBuilder().WithCustomFactory<ReflectionServiceFactory>());

        _lambdaBased = InitCustomContainer(
            new ContainerBuilder().WithCustomFactory<LambdaServiceFactory>());

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
