namespace DependencyInjection.Helpers;

public static class ExceptionsHelper
{
    public static object ThrowServiceSingleConstructor(string serviceName)
        => throw new InvalidOperationException($"Service {serviceName} must have only one constructor");
    
    public static object ThrowServiceNotRegistered(string serviceName)
        => throw new InvalidOperationException($"Service {serviceName} is not registered");
}