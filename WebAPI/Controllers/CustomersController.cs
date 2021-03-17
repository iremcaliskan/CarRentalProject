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
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        ICustomerService _customerService;

        public CustomersController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        [HttpGet("getall")]
        public IActionResult GetAll()
        {
            var result = _customerService.GetAll();
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpGet("getbyid")] // api/customers/getbyid?id=num
        public IActionResult GetById(int id)
        {
            var result = _customerService.GetById(id);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
        
        [HttpGet("getcustomerdetails")] // api/customers/getcustomerdetails
        public IActionResult GetCustomerDetails()
        {
            var result = _customerService.GetCustomerDetails();
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpGet("getcustomersearch")] // api/customers/getcustomersearch?key=str
        public IActionResult GetCustomerDetails(string key)
        {
            var result = _customerService.GetCustomerDetails(key);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpGet("getcustomerdetailsbyid")] // api/customers/getcustomerdetailsbyid?id=num
        public IActionResult GetCustomerDetailsById(int id)
        {
            var result = _customerService.GetCustomerDetailsById(id);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPost("add")]
        public IActionResult Add(Customer customer)
        {
            var result = _customerService.Add(customer);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPost("update")]
        public IActionResult Update(Customer customer)
        {
            var result = _customerService.Update(customer);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPost("delete")]
        public IActionResult Delete(Customer customer)
        {
            var result = _customerService.Delete(customer);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
    }
}
