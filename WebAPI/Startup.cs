using Business.Abstract;
using Business.Concrete;
using Core.Utilities.IoC;
using Core.Utilities.Security.Encryption;
using Core.Utilities.Security.JWT;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
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

            // API'ye bir yerden istek geldiðinde güvenlik tehdidi olarak algýlanýr, izin vermek için:
            services.AddCors(options =>
            {
                options.AddPolicy("AllowOrigin",
                    builder => builder.WithOrigins("http://localhost:3000"));
            });

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            // Sistemde JWT kullanýlacak ayarý:

            var tokenOptions = Configuration.GetSection("TokenOptions").Get<TokenOptions>();

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                            .AddJwtBearer(options =>
                            {
                                options.TokenValidationParameters = new TokenValidationParameters
                                {
                                    ValidateIssuer = true,
                                    ValidateAudience = true,
                                    ValidateLifetime = true,
                                    ValidIssuer = tokenOptions.Issuer,
                                    ValidAudience = tokenOptions.Audience,
                                    ValidateIssuerSigningKey = true,
                                    IssuerSigningKey = SecurityKeyHelper.CreateSecurityKey(tokenOptions.SecurityKey)
                                };
                            });

            ServiceTool.Create(services);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors(builder => builder.WithOrigins("http://localhost:3000").AllowAnyHeader());

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
