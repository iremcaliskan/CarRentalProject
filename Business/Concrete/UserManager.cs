using Business.Abstract;
using Business.Constants;
using Core.Results;
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
        { // Oluşturulma anında bir veri erişim yöntemi isteyecek
            _userDal = userDal;
        }

        public IResult Add(User user)
        {
            if (!user.Email.Contains("@"))
            {
                return new ErrorResult("Email format is wrong!");
            }
            _userDal.Add(user);
            return new SuccessResult(Messages.Added);
        }

        public IResult Update(User user)
        {
            if (!user.Email.Contains("@"))
            {
                return new ErrorResult("Email format is wrong!");
            }
            _userDal.Update(user);
            return new SuccessResult(Messages.Updated);
        }

        public IResult Delete(User user)
        {
            try
            {
                _userDal.Delete(user);
                return new SuccessResult(Messages.Deleted);
            }
            catch (Exception)
            {
                throw new Exception("A system error occurs on deletion!");
            }
        }

        public IDataResult<List<User>> GetAll()
        {
            return new SuccessDataResult<List<User>>(_userDal.GetAll(),Messages.GetAll);
        }

        public IDataResult<User> GetById(int userId)
        {
            return new SuccessDataResult<User>(_userDal.Get(u=> u.UserId == userId), Messages.GetUserByUserId);
        }
    }
}
