using System;

namespace UniDependencyInjection.Helpers
{
    public static class ExceptionsHelper
    {
        public static object ThrowServiceSingleConstructor(string serviceName)
            => throw new InvalidOperationException($"Service {serviceName} must have only one constructor");
    
        public static object ThrowServiceNotRegistered(string serviceName)
            => throw new InvalidOperationException($"Service {serviceName} is not registered");

        public static object ThrowAsyncDisposeInInvalidContext()
            => throw new InvalidOperationException("Can not use async dispose in default dispose context");

        public static object ThrowServiceFactoryAlreadyExists()
            => throw new InvalidOperationException("Can not set service factory multiple times");

        public static object ThrowFunctionArgumentsCount(int target) 
            => throw new InvalidOperationException($"Function must have {target} params count");

        public static object ThrowUnhandledMonoInjectionType() 
            => throw new InvalidOperationException("Method with mono factory attribute must return game object or component");

        public static object ThrowMultipleContainersNotSupported() 
            => throw new InvalidOperationException("Multiple containers are not supported");
    }
}

