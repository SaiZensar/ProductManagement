using DataLayer;
using DataLayer.Services;
using DomainModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProductManagement.Data;
using ProductManagement.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace ProductManagement.ApiControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductData data;
        private readonly ProdDbContext _db;
        private readonly IWebHostEnvironment webHostEnvironment;
        public ProductsController(IProductData _data, ProdDbContext db, IWebHostEnvironment hostEnvironment)
        {
            data = _data;
            _db = db;
            webHostEnvironment = hostEnvironment;
        }
        [HttpGet]
        public IActionResult Get()
        {
            var model = data.GetAll();
            if (model == null)
            {
                return NotFound();
            }
            return Ok(model);
        }
        [HttpGet("{id}")]
        [Route("GetById/{id}")]

        public IActionResult GetById(int id)
        {
            var singleData = _db.Products.FirstOrDefault(a => a.Id == id);
            if (singleData != null)
            {
                return Ok(singleData);
            }
            else
            {
                return NotFound();
            }
        }
        [HttpPost]
        public IActionResult Post([FromBody] Product product)
        {
            if (product == null)
            {
                return NotFound();
            }
            else
            {

                _db.Products.Add(product);

                return Ok();
            }
        }


        [HttpPut("{id}")]

        public IActionResult Put(int id, [FromBody] Product product)
        {
            var res = _db.Products.FirstOrDefault(e => e.Id == id);
            if (product == null)
            {
                return BadRequest($"Product with id {id.ToString()} not found");
            }
            else
            {
                res.Id = product.Id;
                res.Name = product.Name;
                res.Code = product.Code;
                res.Description = product.Description;
                res.Available = product.Available;
                res.Price = product.Price;
                _db.SaveChanges();
                return Ok(res);
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            if (id <= 0)
                return BadRequest("Not a valid Character id");


            var s = _db.Products
                .Where(s => s.Id == id)
                .FirstOrDefault();

            _db.Entry(s).State = Microsoft.EntityFrameworkCore.EntityState.Deleted;
            _db.SaveChanges();
            return Ok(s);
        }
    }
}