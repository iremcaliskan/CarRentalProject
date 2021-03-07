using Core.Utilities.Results;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Abstract
{
    public interface IColorService
    {
        IDataResult<List<Color>> GetAll(); // Tüm renkleri listele + operasyonun başarı durumuna göre mesaj ver
        IDataResult<Color> GetById(int colorId); // ColorId'ye göre ürün getir + operasyon başarı durumuna göre de mesaj ver
        IResult Add(Color color); // void yerine IResult
        IResult Update(Color color); // void yerine IResult
        IResult Delete(Color color); // void yerine IResult
    }
}
