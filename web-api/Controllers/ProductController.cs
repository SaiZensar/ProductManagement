using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using web_api.Contracts;
using web_api.Model;

namespace web_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _service;

        public ProductController(IProductService service)
        {
            _service = service;
        }

        // GET api/shoppingcart
        [HttpGet]
        public ActionResult<IEnumerable<Product>> Get()
        {
            var items = _service.GetAllItems();
            return Ok(items);
        }

        // GET api/shoppingcart/5
        [HttpGet("{id}")]
        public ActionResult<Product> Get(Guid id)
        {
            var item = _service.GetById(id);

            if (item == null)
            {
                return NotFound();
            }

            return Ok(item);
        }

        // POST api/shoppingcart
        [HttpPost]
        public ActionResult Post([FromBody] Product value)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var item = _service.Add(value);
            return CreatedAtAction("Get", new { id = item.Id }, item);
        }

        // DELETE api/shoppingcart/5
        [HttpDelete("{id}")]
        public ActionResult Remove(Guid id)
        {
            var existingItem = _service.GetById(id);

            if (existingItem == null)
            {
                return NotFound();
            }

            _service.Remove(id);
            return Ok();
        }
    }
}