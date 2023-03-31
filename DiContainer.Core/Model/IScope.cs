﻿using System;

namespace DiContainer.Core.Model
{
    public interface IScope : IDisposable, IAsyncDisposable
    {
        public TService Resolve<TService>();
    
        public object Resolve(Type serviceType);
    }
}
