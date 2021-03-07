using Castle.DynamicProxy;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Utilities.Interceptors
{
    // Bu Attribute, sınıflara veya metotlara eklenebilir, birden fazla eklenebilir, inherit edilirse de çalışır
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true, Inherited = true)]
    public abstract class MethodInterceptionBaseAttribute : Attribute, IInterceptor
    { // IInterceptor, Autofac yapısından geliyor.
        public int Priority { get; set; } // Öncelik, hangi Attribute önce çalışsın

        public virtual void Intercept(IInvocation invocation) // IInvocation, Autofac yapısından geliyor.
        {
            // Ezilebilir metot
        }
    }
}
