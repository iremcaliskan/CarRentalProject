using Core.Utilities.Results;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Abstract
{
    public interface IBrandService
    {
        IDataResult<List<Brand>> GetAll(); // Tüm markaları listele + operasyon başarı durumuna görede mesaj ver
        IDataResult<Brand> GetById(int brandId); // BrandId'ye göre ürün getir + operasyon başarı durumuna göre de mesaj ver
        IDataResult<List<Brand>> GetByName(string brandName); // Markalar içerinde search

        IResult Add(Brand brand); // void -- IResult
        IResult Update(Brand brand); // void -- IResult
        IResult Delete(Brand brand); // void -- IResult
    }
}
