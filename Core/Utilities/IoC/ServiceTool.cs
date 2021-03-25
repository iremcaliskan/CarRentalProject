using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Utilities.IoC
{ // Inversion of Control
    public static class ServiceTool
    {
        public static IServiceProvider ServiceProvider { get; private set; }

        public static IServiceCollection Create(IServiceCollection services)
        {
            // Take the services and build them, that provides Injections
            ServiceProvider = services.BuildServiceProvider();
            return services;
        }
    }
}
