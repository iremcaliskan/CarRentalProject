using Core.Utilities.Results;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Abstract
{
    public interface ICarService
    {
        IDataResult<List<Car>> GetAll();
        IDataResult<Car> GetById(int carId);
        IDataResult<List<Car>> GetCarsByBrandId(int brandId);
        IDataResult<List<Car>> GetCarsByColorId(int colorId);

        IResult Add(Car car);
        IResult Update(Car car);
        IResult Delete(Car car);

        IDataResult<List<CarDetailDto>> GetCarDetails(); // Verilen özellik listesine göre(join) arabaların detaylarını getir + operasyonun başarı durumuna göre mesaj ver

        IDataResult<CarDetailDto>GetCarDetailsById(int carId);
        IDataResult<List<CarImagesDto>> GetCarImageDetails();
        IDataResult<List<CarImagesDto>> GetCarImageDetailsByCarId(int carId);

    }
}
