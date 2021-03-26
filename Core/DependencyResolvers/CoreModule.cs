using Core.CrossCuttingConcerns.Caching;
using Core.CrossCuttingConcerns.Caching.Microsoft;
using Core.Utilities.IoC;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace Core.DependencyResolvers
{
    public class CoreModule : ICoreModule
    {
        public void Load(IServiceCollection serviceCollection)
        {
            // Create an instance of IMemoryCache at runtime
            serviceCollection.AddMemoryCache(); // This dependency is needed for IMemoryCache to work in the system
            serviceCollection.AddSingleton<IHttpContextAccessor, HttpContextAccessor>(); // The context created in every request makes it possible to track the user's request.
            serviceCollection.AddSingleton<ICacheManager, MemoryCacheManager>(); // Injection can not be written in Constructure as Constructure Injection. It will be an Aspect.
            serviceCollection.AddSingleton<Stopwatch>(); // Timer
        }
    }
}
