using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.CrossCuttingConcerns.Validation
{
    public static class ValidationTool
    {
        //  Generic validation tool for any object
        // <param name="validator"></param>
        // <param name="entity"></param>
        public static void Validate(IValidator validator, object entity)
        {
            var context = new ValidationContext<object>(entity); // Gelen entity için Object türünde Doğrulama Context'i oluştur.
            var result = validator.Validate(context); // Gelen Validator ile contexti doğrulayacak

            if (!result.IsValid)
            {
                throw new ValidationException(result.Errors);
            }
        }
    }
}
