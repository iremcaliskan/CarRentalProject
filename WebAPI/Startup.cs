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

            //services.AddSingleton<ICarService, CarManager>(); // Arkaplanda referans olu�turur, newler
            //services.AddSingleton<ICarDal, EfCarDal>(); // ICarDal ba��ml�l��� g�r�rsen anlam� EfCarDal

            // Autofac/Ninject/CastleWindsor/StructureMap/LightInject/DryInject gibi yap�lar IoC Container mimarisi sunar.
            // Yukar�daki yap� Autofac'e �evrildi,
            // Autofac de single instance �retimini sa�layan ayn� i�i yap�yor.
            /* Neden bu yap�ld�? - �leride birden fazla API eklenirse, farkl� servis yap�lar� mimarileri 
             * eklenirse t�m yap�land�rma ayarlar� bu Startup s�n�f�nda kal�r.
             * Tekrar tekrar yaz�lamas�n�n �nlenmesi i�in Business'a eklenerek kullan�ma a��k hale
             * getirilmelidir.
             * Dependency Resolvers: Loosely couple ba��l�l���(interface injection) ��z�mleme i�lemleri.
             * .Net'in IoC yap�land�r�lmas� kullan�lmad��� i�in AutofacBusinessModule olarak yaz�lan
             * IoC Container kullan�lacak.
             */

            // API'ye bir yerden istek geldi�inde g�venlik tehdidi olarak alg�lan�r, izin vermek i�in:
            services.AddCors(options =>
            {
                options.AddPolicy("AllowOrigin",
                    builder => builder.WithOrigins("http://localhost:3000"));
            });

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            // Sistemde JWT kullan�lacak ayar�:

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
