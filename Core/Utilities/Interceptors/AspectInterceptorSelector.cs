using Castle.DynamicProxy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Core.Utilities.Interceptors
{
    public class AspectInterceptorSelector : IInterceptorSelector
    { // Araya girme seçicisi
        public IInterceptor[] SelectInterceptors(Type type, MethodInfo method, IInterceptor[] interceptors)
        {
            // Class'ın Attributelarını bul
            var classAttributes = type.GetCustomAttributes<MethodInterceptionBaseAttribute>
                (true).ToList();

            // Method'un Attributelarını bul
            var methodAttributes = type.GetMethod(method.Name)
                .GetCustomAttributes<MethodInterceptionBaseAttribute>(true);

            // Class'ın Attributelarını bir listeye koy
            classAttributes.AddRange(methodAttributes);

            // Class'ın Attributelarının çalışma sırasınıda öncelik değerine göre sırala
            return classAttributes.OrderBy(x => x.Priority).ToArray();
        }
    }
}
