using Core.Utilities.Results;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Abstract
{
    public interface IBrandService
    {
        IDataResult<List<Brand>> GetAll(); // Tüm markaları listele // Tüm markaları listele + operasyon başarı durumuna görede mesaj ver
        IDataResult<Brand> GetById(int brandId); // BrandId'ye göre ürün getir + operasyon başarı durumuna göre de mesaj ver
        IResult Add(Brand brand); // void yerine IResult
        IResult Update(Brand brand); // void yerine IResult
        IResult Delete(Brand brand); // void yerine IResult
    }
}
