using Customer.ServiceContract;
using DataLayer;
using DomainModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProductManagement.Uitility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductManagement.Controllers
{
    public class CustomerController : Controller
    {
        private readonly ICustomerService customerService;

        public CustomerController(ICustomerService _cs)
        {
            customerService = _cs;
        }
        public IActionResult Index()
        {
            return View(customerService.ListOfProducts());
        }
        [HttpGet]
        public IActionResult Index(string prodSearch)
        {
            ViewData["GetProductDetails"] = prodSearch;
            
            var prodquery = customerService.Search(prodSearch);
            return View(prodquery);
        }
        [Authorize]
        public IActionResult Details(int id)
        {
            var data = customerService.Details(id);
            return View(data);
        }
        public IActionResult List(string prodCode)
        {
            /*IEnumerable<Product> products;
            products = db.Products.Where(p => p.Code.Contains(prodCode)).OrderBy(p => p.Id);
            return View(products);*/

            var pl = customerService.List(prodCode);
            return View(pl);
        }

        public IActionResult ListOfProducts()
        {
            return View(customerService.ListOfProducts());
        }
    }
}
