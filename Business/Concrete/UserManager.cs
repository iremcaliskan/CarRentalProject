using Business.Abstract;
using Business.BusinessAspect.Autofac;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Transaction;
using Core.Aspects.Autofac.Validation;
using Core.Entities.Concrete;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Concrete
{
    public class UserManager : IUserService
    {
        IUserDal _userDal;
        public UserManager(IUserDal userDal)
        {
            _userDal = userDal;
        }

        [ValidationAspect(typeof(UserValidator))]
        [CacheRemoveAspect("IUserService.Get")]
        [TransactionScopeAspect]
        public IResult Add(User user)
        {
            /*
            if (!user.Email.Contains("@"))
            {
                return new ErrorResult("Email format is wrong!");
            }
            */

            _userDal.Add(user);
            return new SuccessResult(Messages.Added);
        }

        [ValidationAspect(typeof(UserValidator))]
        [CacheRemoveAspect("IUserService.Get")]
        [TransactionScopeAspect]
        public IResult Update(User user)
        {
            /*
            if (!user.Email.Contains("@"))
            {
                return new ErrorResult("Email format is wrong!");
            }
            */

            _userDal.Update(user);
            return new SuccessResult(Messages.Updated);
        }

        [CacheRemoveAspect("IUserService.Get")]
        [TransactionScopeAspect]
        public IResult Delete(User user)
        {
            try
            {
                _userDal.Delete(user);
                return new SuccessResult(Messages.Deleted);
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }
        }

        [SecuredOperation("user.list")]
        [CacheAspect]
        public IDataResult<List<User>> GetAll()
        {
            return new SuccessDataResult<List<User>>(_userDal.GetAll(),Messages.GetAll);
        }

        [SecuredOperation("user.getid")]
        [CacheAspect]
        public IDataResult<User> GetById(int userId)
        {
            return new SuccessDataResult<User>(_userDal.Get(u=> u.UserId == userId), Messages.GetUserByUserId);
        }

        [CacheAspect]
        public IDataResult<List<OperationClaim>> GetClaims(User user)
        {
            return new SuccessDataResult<List<OperationClaim>>(_userDal.GetClaims(user), Messages.UserClaimsListed);
        }

        public IDataResult<User> GetByMail(string email)
        {
            return new SuccessDataResult<User>(_userDal.Get(u => u.Email == email), Messages.UserFoundByEmail);
        }
    }
}
