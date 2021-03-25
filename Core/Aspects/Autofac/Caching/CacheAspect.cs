using Core.CrossCuttingConcerns.Caching;
using Core.Utilities.Interceptors;
using Core.Utilities.IoC;
using System;
using Microsoft.Extensions.DependencyInjection; // needs for GetService<ICacheManager>()
using System.Text;
using Castle.DynamicProxy;
using System.Linq;

namespace Core.Aspects.Autofac.Caching
{
    public class CacheAspect : MethodInterception // Aspect, Attribute
    {
        private int _duration;
        private ICacheManager _cacheManager;

        public CacheAspect(int duration = 60) // Default duration is 60 minutes 
        {
            _duration = duration;
            _cacheManager = ServiceTool.ServiceProvider.GetService<ICacheManager>(); // No Constructure Injection for Aspects
        }

        public override void Intercept(IInvocation invocation) // invocation -- method
        { 
            // Key Creation for Cache:
            var methodName = string.Format($"{invocation.Method.ReflectedType.FullName}.{invocation.Method.Name}"); // Get method's namespace + class name + method name
            // Ex. Business.Concrete.IProductService.GetAll

            var arguments = invocation.Arguments.ToList(); // Get params of method to list
            var key = $"{methodName}({string.Join(",", arguments.Select(x => x?.ToString() ?? "<Null>"))})"; // If param value(s) exist join them and add to method
            // Ex. key = Istanbul, 5

            // If Cache is exist in memory, Get the Cache by own key
            if (_cacheManager.IsAdd(key))
            { // Is there a Cache key in memory?
                invocation.ReturnValue = _cacheManager.Get(key); // Method's return value come from database normally, but cache is exist in memory, there is no need db for this operation
                return;
            }

            // If there is no Cache in Memory, get datas from db and create Cache
            invocation.Proceed();
            _cacheManager.Add(key, invocation.ReturnValue, _duration);
        }
    }
}
