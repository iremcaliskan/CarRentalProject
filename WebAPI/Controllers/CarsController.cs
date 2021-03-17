using Business.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")] //api/cars yazılarak ulaşılır.
    [ApiController] // api controller olduğunu belirtir
    public class CarsController : ControllerBase
    { 
        // RESTFUL --> JSON
        // IoC Container: Bellekte bir yer içerisine referansları koymak
        ICarService _carService;

        public CarsController(ICarService carService) // Constructure Injection ile bağımlılık en aza indirildi, loosely coupled
        {
            _carService = carService;
        }

        [HttpGet("getall")] // api/cars/getall
        public IActionResult GetAll()
        {
            var result = _carService.GetAll();
            if (result.Success)
            {
                return Ok(result); // Created 201 de olabilirdi ama HttpGet ile daha çok Ok 200 kullanılır.
                // result ile hem Data hem success hemde mesaj 
            }
            return BadRequest(result);
        }

        [HttpGet("getbyid")] // api/cars/getbyid?id=num
        public IActionResult GetById(int id)
        {
            var result = _carService.GetById(id);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpGet("getbybrandid")] // api/cars/getbybrandid?id=num
        public IActionResult GetByBrandId(int id)
        {
            var result = _carService.GetCarsByBrandId(id);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpGet("getbycolorid")] // api/cars/getbycolorid?id=num
        public IActionResult GetByColorId(int id)
        {
            var result = _carService.GetCarsByColorId(id);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpGet("getcardetails")] // api/cars/getcarsdetails
        public IActionResult GetCarDetails()
        {
            var result = _carService.GetCarDetails();
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpGet("getcardetailsbyid")] // api/cars/getcarsdetailsbyid?id=num
        public IActionResult GetCarDetailsById(int id)
        {
            var result = _carService.GetCarDetailsById(id);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
        
        [HttpGet("getcarimagedetails")] // api/cars/getcarimagedetails
        public IActionResult GetCarImageDetails()
        {
            var result = _carService.GetCarImageDetails();
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpGet("getcarimagedetailsbycarid")] // api/cars/getcarimagedetailsbycarid?id=num
        public IActionResult GetCarImageDetailsByCarId(int id)
        {
            var result = _carService.GetCarImageDetailsByCarId(id);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPost("add")] // api/cars/add
        public IActionResult Add(Car car)
        {
            var result = _carService.Add(car);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
        
        [HttpPost("update")] // HttpPut da olabilir
        public IActionResult Update(Car car)
        {
            var result = _carService.Update(car);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPost("delete")] // HttpDelete de olabilir
        public IActionResult Delete(Car car)
        {
            var result = _carService.Delete(car);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
    }
}
