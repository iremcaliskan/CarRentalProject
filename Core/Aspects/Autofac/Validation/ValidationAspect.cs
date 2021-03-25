using Castle.DynamicProxy;
using Core.CrossCuttingConcerns.Validation;
using Core.Utilities.Interceptors;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core.Aspects.Autofac.Validation
{
    public class ValidationAspect: MethodInterception
    { // Doğrulama bir araya girme işlemidir.
        private Type _validatorType; //Doğrulama işlemi için doğrulayıcı tipi vardır.

        public ValidationAspect(Type validatorType)
        { // Oluşturulma anında doğrulayıcı tipi alır.
            if (!typeof(IValidator).IsAssignableFrom(validatorType))
            { // Gönderilen Validator bir IValidator değilse yani AbstractValidator değilse, hata ver
                throw new Exception("It is not a validator class!");
            }
            _validatorType = validatorType; // Doğrulayıcı set edilir.
        }

        // Execute before method and validate parameters of method
        // <param name="invocation"></param>
        protected override void OnBefore(IInvocation invocation)
        { // OnBefore ezildi,
            var validator = (IValidator)Activator.CreateInstance(_validatorType); // Reflection, çalışma anında newle,
            var entityType = _validatorType.BaseType.GetGenericArguments()[0]; // Validator'ın Base tipine git, aldığı generic sınıflardan ilkinin tipini bul
            var entities = invocation.Arguments.Where(t => t.GetType() == entityType);
            // Metotun parametre tipi/tipleri ile base'in generic tipi aynı olanı bul ve listeye al

            foreach (var entity in entities)
            { // Tipi/Tipleri gez tek tek ve doğrula
                ValidationTool.Validate(validator,entity);
            }
        }
    }
}
