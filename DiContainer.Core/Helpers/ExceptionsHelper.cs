using System;

namespace DiContainer.Core.Helpers
{
    public static class ExceptionsHelper
    {
        public static object ThrowServiceSingleConstructor(string serviceName)
            => throw new InvalidOperationException($"Service {serviceName} must have only one constructor");
    
        public static object ThrowServiceNotRegistered(string serviceName)
            => throw new InvalidOperationException($"Service {serviceName} is not registered");

        public static object ThrowAsyncDisposeInInvalidContext()
            => throw new InvalidOperationException("Can not use async dispose in default dispose context");

        public static object ThrowServiceFactoryAlreadyAdded()
            => throw new InvalidOperationException("Can not set service factory multiple times");
    }
}

