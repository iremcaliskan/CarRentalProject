using Core.Results;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Abstract
{
    public interface ICarService
    {
        IDataResult<List<Car>> GetAll(); // Tüm arabaları listele + operasyonun başarı durumuna göre mesaj ver
        IDataResult<Car> GetById(int carId); // CarId'ye göre araba getir + operasyonun başarı durumuna göre mesaj ver
        IDataResult<List<Car>> GetCarsByBrandId(int brandId); // BrandId'ye göre araba listesini getir + operasyonun başarı durumuna göre mesaj ver
        IDataResult<List<Car>> GetCarsByColorId(int colorId); // ColorId'ye göre araba listesini getir + operasyonun başarı durumuna göre mesaj ver

        IResult Add(Car car); // void yerine IResult
        IResult Update(Car car); // void yerine IResult
        IResult Delete(Car car); // void yerine IResult

        IDataResult<List<CarDetailDto>> GetCarDetails(); // Verilen özellik listesine göre(join) arabaların detaylarını getir + operasyonun başarı durumuna göre mesaj ver

        IDataResult<CarDetailDto>GetCarDetailsById(int carId); // Özel metodu

    }
}
