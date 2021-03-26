using Business.Abstract;
using Business.BusinessAspect.Autofac;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Transaction;
using Core.Aspects.Autofac.Validation;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Concrete
{
    public class ColorManager : IColorService
    {
        IColorDal _colorDal;
        public ColorManager(IColorDal colorDal)
        {
            _colorDal = colorDal;
        }

        [SecuredOperation("color.add")]
        [ValidationAspect(typeof(ColorValidator))]
        [CacheRemoveAspect("IColorService.Get")]
        [TransactionScopeAspect]
        public IResult Add(Color color)
        {
            /*
            if (color.ColorName.Length < 3)
            {
                return new ErrorResult(Messages.ColorCanNotAdded);
            }
            */

            _colorDal.Add(color);
            return new SuccessResult(Messages.Added);
        }

        [SecuredOperation("color.update")]
        [ValidationAspect(typeof(ColorValidator))]
        [CacheRemoveAspect("IColorService.Get")]
        [TransactionScopeAspect]
        public IResult Update(Color color)
        {
            /*
            if (color.ColorName.Length < 3)
            {
                return new ErrorResult(Messages.ColorCanNotUpdated);
            }
            */

            _colorDal.Update(color);
            return new SuccessResult(Messages.Updated);
        }

        [SecuredOperation("color.delete")]
        [CacheRemoveAspect("IColorService.Get")]
        [TransactionScopeAspect]
        public IResult Delete(Color color)
        {
            try
            {
                _colorDal.Delete(color);
                return new SuccessResult(Messages.Deleted);
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }       
        }

        [SecuredOperation("color.list")]
        public IDataResult<List<Color>> GetAll()
        {
            return new SuccessDataResult<List<Color>>(_colorDal.GetAll(), Messages.GetAll);
        }

        [SecuredOperation("color.getid")]
        [CacheAspect]
        public IDataResult<Color> GetById(int colorId)
        {
            return new SuccessDataResult<Color>(_colorDal.Get(c => c.ColorId == colorId), Messages.GetColorByColorId);
        }

        [SecuredOperation("color.getname")]
        public IDataResult<List<Color>> GetByName(string key)
        {
            return new SuccessDataResult<List<Color>>(_colorDal.GetAll(c => c.ColorName.Contains(key)), Messages.ColorIsFoundByKey);
        }
    }
}
