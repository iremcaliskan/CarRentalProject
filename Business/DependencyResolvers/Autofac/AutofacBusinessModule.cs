﻿using Autofac;
using Autofac.Extras.DynamicProxy;
using Business.Abstract;
using Business.Concrete;
using Castle.DynamicProxy;
using Core.Utilities.Interceptors;
using Core.Utilities.Security.JWT;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.DependencyResolvers.Autofac
{ // İleride Bağımlılık Çözümleme Autofac dışında başka bir teknoloji ile yapılırsa diye, örneğin Postsharp,  klasörlendi.
    // Autofac dependency injection Module
    public class AutofacBusinessModule : Module // Autofac Module
    {
        // WebAPI deki Startup sınıfında yapılan bağımlık çözme işlemlerini Autofac ortamında yapmak

        protected override void Load(ContainerBuilder builder)
        { // Yükleme, uygulama hayata geçtiği zaman çalışır, bir IoC Container'ı oluşturur.
            // IoC Container, arka planda referans oluşturur, newler, bir bağımlılık varsa belirtilen tipte yanındaki karşılığı olacaktır.

            // Business injections
            // services.AddSingleton<ICarService, CarManager>(); aynı işlem:
            builder.RegisterType<CarManager>().As<ICarService>().SingleInstance();
            // ICarService'i isteyen olursa, CarManager'ın instance'ını ver.
            builder.RegisterType<UserManager>().As<IUserService>().SingleInstance();
            builder.RegisterType<CustomerManager>().As<ICustomerService>().SingleInstance();
            builder.RegisterType<ColorManager>().As<IColorService>().SingleInstance();
            builder.RegisterType<BrandManager>().As<IBrandService>().SingleInstance();
            builder.RegisterType<RentalManager>().As<IRentalService>().SingleInstance();
            builder.RegisterType<CarImageManager>().As<ICarImageService>().SingleInstance();
            builder.RegisterType<UserManager>().As<IUserService>().SingleInstance();

            builder.RegisterType<AuthManager>().As<IAuthService>().SingleInstance();
            builder.RegisterType<JwtHelper>().As<ITokenHelper>().SingleInstance();

            //DataAccess injections
            builder.RegisterType<EfCarDal>().As<ICarDal>().SingleInstance();
            // ICarDal'ı isteyen olursa, EfCarDal'ın instance'ını ver.
            builder.RegisterType<EfUserDal>().As<IUserDal>().SingleInstance();
            builder.RegisterType<EfBrandDal>().As<IBrandDal>().SingleInstance();
            builder.RegisterType<EfCustomerDal>().As<ICustomerDal>().SingleInstance();
            builder.RegisterType<EfColorDal>().As<IColorDal>().SingleInstance();
            builder.RegisterType<EfRentalDal>().As<IRentalDal>().SingleInstance();
            builder.RegisterType<EfCarImageDal>().As<ICarImageDal>().SingleInstance();
            builder.RegisterType<BrandManager>().As<IBrandService>().SingleInstance();
            builder.RegisterType<EfBrandDal>().As<IBrandDal>().SingleInstance();
            builder.RegisterType<EfUserDal>().As<IUserDal>().SingleInstance();


            // .Net'e kendi IoC yapılandırması yerine AutofacBusinessModule'ünün kullanılcağı belirtildi.
            // Program.cs'e Cofiguration yapıldı.

            builder.RegisterType<HttpContextAccessor>().As<IHttpContextAccessor>();

            var assembly = System.Reflection.Assembly.GetExecutingAssembly(); // Çalışan uygulama içerisinde

            builder.RegisterAssemblyTypes(assembly).AsImplementedInterfaces() // implemente edilmiş Interfaceler bulunur
                .EnableInterfaceInterceptors(new ProxyGenerationOptions()
                {
                    Selector = new AspectInterceptorSelector() // Onlar için AspectInterceptorSelector'ı çağır.
                }).SingleInstance();
            // Yani, Autofac, bütün sınıflar için önce Aspect'i var mı bakar sonra kodun çalışmasını sağlar.
        }

    }
}
