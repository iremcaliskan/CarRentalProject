﻿using Business.Abstract;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects;
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
        { // Oluşturulma anında bir veri erişim yöntemi ister
            _colorDal = colorDal;
        }

        [ValidationAspect(typeof(ColorValidator))]
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

        [ValidationAspect(typeof(ColorValidator))]
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

        public IResult Delete(Color color)
        {
            try
            {
                _colorDal.Delete(color);
                return new SuccessResult(Messages.Deleted);
            }
            catch (Exception)
            {
                throw new Exception("A system error occurs on deletion!");
            }       
        }

        public IDataResult<List<Color>> GetAll()
        {
            return new SuccessDataResult<List<Color>>(_colorDal.GetAll(), Messages.GetAll);
        }

        public IDataResult<Color> GetById(int colorId)
        {
            return new SuccessDataResult<Color>(_colorDal.Get(c => c.ColorId == colorId), Messages.GetColorByColorId);
        }

        public IDataResult<List<Color>> GetByName(string key)
        {
            return new SuccessDataResult<List<Color>>(_colorDal.GetAll(c => c.ColorName.Contains(key)), "Color is found by key!");
        }
    }
}
