using Autofac;
using Autofac.Extensions.DependencyInjection;
using Business.DependencyResolvers.Autofac;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        /*
        * Set default ioc container to autofac
        */
        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args) // Yayýn ortamý, server configuration
            .UseServiceProviderFactory(new AutofacServiceProviderFactory()) // Servis saðlayýcý fabrikasý olarak AutofacServiceProviderFactory
             .ConfigureContainer<ContainerBuilder>(builder =>
             {
                 builder.RegisterModule(new AutofacBusinessModule());
             }) // .Net Core yerine baþka bir IoC için yazýlacak kod, dokümandan
            .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
