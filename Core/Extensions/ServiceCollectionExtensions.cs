using Core.Utilities.IoC;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Extensions
{
    public static class ServiceCollectionExtensions
    { // This class extends the IServiceCollection
        public static IServiceCollection AddDependencyResolvers(this IServiceCollection serviceCollection, ICoreModule[] modules) // this IServiceCollection serviceCollection : Service Collections to expand
        {
            foreach (var module in modules)
            { // Loads the each module in modules 
                module.Load(serviceCollection);
            }

            return ServiceTool.Create(serviceCollection); // Create the Service Collections to expand
        }
    }
}
