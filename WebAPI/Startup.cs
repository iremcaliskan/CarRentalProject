using Business.Abstract;
using Business.Concrete;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            //services.AddSingleton<ICarService, CarManager>(); // Arkaplanda referans oluþturur, newler
            //services.AddSingleton<ICarDal, EfCarDal>(); // ICarDal baðýmlýlýðý görürsen anlamý EfCarDal

            //services.AddSingleton<IBrandService, BrandManager>();
            //services.AddSingleton<IBrandDal, EfBrandDal>();

            //services.AddSingleton<IColorService, ColorManager>();
            //services.AddSingleton<IColorDal, EfColorDal>();

            //services.AddSingleton<ICustomerService, CustomerManager>();
            //services.AddSingleton<ICustomerDal, EfCustomerDal>();

            //services.AddSingleton<IRentalService, RentalManager>();
            //services.AddSingleton<IRentalDal, EfRentalDal>();

            //services.AddSingleton<IUserService, UserManager>();
            //services.AddSingleton<IUserDal, EfUserDal>();

            // Autofac/Ninject/CastleWindsor/StructureMap/LightInject/DryInject gibi yapýlar IoC Container mimarisi sunar.
            // Yukarýdaki yapý Autofac'e çevrildi,
            // Autofac de single instance üretimini saðlayan ayný iþi yapýyor.
            /* Neden bu yapýldý? - Ýleride birden fazla API eklenirse, farklý servis yapýlarý mimarileri 
             * eklenirse tüm yapýlandýrma ayarlarý bu Startup sýnýfýnda kalýr.
             * Tekrar tekrar yazýlamasýnýn önlenmesi için Business'a eklenerek kullanýma açýk hale
             * getirilmelidir.
             * Dependency Resolvers: Loosely couple baðýlýlýðý(interface injection) çözümleme iþlemleri.
             * .Net'in IoC yapýlandýrýlmasý kullanýlmadýðý için AutofacBusinessModule olarak yazýlan
             * IoC Container kullanýlacak.
             */

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
