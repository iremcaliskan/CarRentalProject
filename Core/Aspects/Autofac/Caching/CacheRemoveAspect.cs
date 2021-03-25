using Core.CrossCuttingConcerns.Caching;
using Core.Utilities.Interceptors;
using Core.Utilities.IoC;
using System;
using Microsoft.Extensions.DependencyInjection;
using System.Text;
using Castle.DynamicProxy;

namespace Core.Aspects.Autofac.Caching
{
    public class CacheRemoveAspect : MethodInterception // Attribute
    { // CacheRemove is used for when the data is changed, Changes: CRUD, Manipulation Methods over Data(s)
        private string _pattern;
        private ICacheManager _cacheManager;

        public CacheRemoveAspect(string pattern)
        {
            _pattern = pattern;
            _cacheManager = ServiceTool.ServiceProvider.GetService<ICacheManager>();
        }

        protected override void OnSuccess(IInvocation invocation)
        { // When the manipulation is successful, remove the Cache. onSuccess MethodInterception
            _cacheManager.RemoveByPattern(_pattern);
        }
    }
}
