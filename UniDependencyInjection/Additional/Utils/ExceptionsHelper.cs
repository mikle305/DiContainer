using System;

namespace UniDependencyInjection
{
    public static class ExceptionsHelper
    {
        public static void ThrowServiceSingleConstructor(string serviceName)
            => throw new InvalidOperationException($"Service {serviceName} must have only one constructor");
    
        public static void ThrowServiceNotRegistered(Type serviceType)
            => throw new InvalidOperationException($"Service {serviceType.Name} is not registered");

        public static void ThrowAsyncDisposeInInvalidContext()
            => throw new InvalidOperationException("Can not use async dispose in default dispose context");

        public static void ThrowServiceFactoryAlreadyExists()
            => throw new InvalidOperationException("Can not set service factory multiple times");

        public static void ThrowCtorArgsCount(Type type, int args) 
            => throw new InvalidOperationException($"{type.Name} constructor must have {args} parameters");
    }
}

