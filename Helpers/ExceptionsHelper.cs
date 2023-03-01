namespace DependencyInjection.Helpers;

public static class ExceptionsHelper
{
    public static object ThrowServiceFactoryReturnsNull() 
        => throw new InvalidOperationException("Service factory can not return null");
    
    public static object ThrowServiceSingleConstructor(string serviceName)
        => throw new InvalidOperationException($"Service {serviceName} must have only one constructor");
}