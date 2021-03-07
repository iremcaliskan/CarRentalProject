using Entities.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.ValidationRules.FluentValidation
{
    public class UserValidator : AbstractValidator<User>
    {
        public UserValidator()
        {
            //RuleFor(u => u.Email).EmailAddress();
            RuleFor(u => u.Email).NotEmpty();
            RuleFor(u => u.Email).Must(ContainAt).WithMessage("Email has to contain @ and .");
        }

        private bool ContainAt(string arg)
        {
            return arg.Contains("@") && arg.Contains(".");
        }
    }
}
