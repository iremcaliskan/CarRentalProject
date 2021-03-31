using Core.DependencyResolvers;
using Core.Extensions;
using Core.Utilities.IoC;
using Core.Utilities.Security.Encryption;
using Core.Utilities.Security.JWT;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;

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

            // Autofac/Ninject/CastleWindsor/StructureMap/LightInject/DryInject for IoC Container Architecture
            // Autofac is used in this project

            /* Neden bu yap�ld�? - �leride birden fazla API eklenirse, farkl� servis yap�lar� mimarileri 
             * eklenirse t�m yap�land�rma ayarlar� bu Startup s�n�f�nda kal�r.
             * Tekrar tekrar yaz�lamas�n�n �nlenmesi i�in Business'a eklenerek kullan�ma a��k hale
             * getirilmelidir.
             * Dependency Resolvers: Loosely couple ba��l�l���(interface injection) ��z�mleme i�lemleri.
             * .Net'in IoC yap�land�r�lmas� kullan�lmad��� i�in AutofacBusinessModule olarak yaz�lan
             * IoC Container kullan�lacak.
             */

            // Every request is percieved a threat by the system come to API. To allow them:
            services.AddCors(options =>
            {
                options.AddPolicy("AllowOrigin",
                    builder => builder.WithOrigins("http://localhost:3000"));
            });

            // JWT Configuration:
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


            // The AddDependendyResolvers structure was established for the CoreModule added here and the modules to be added in the future.
            services.AddDependencyResolvers(new ICoreModule[] { // params can be used too
                new CoreModule()
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        { // Middlewares
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors(builder => builder.WithOrigins("http://localhost:3000").AllowAnyHeader());

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication(); // Claim, Roles

            app.UseAuthorization(); // Key

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
